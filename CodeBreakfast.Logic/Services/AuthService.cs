using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CodeBreakfast.Logic.Services;

public class AuthService(IAuthRepository authRepository, UserManager<User> userManager, IConfiguration configuration) : IAuthService
{
    public async Task<ApiResponse<SessionModel>> Login(LoginDto loginDto)
    {
        var response = new ApiResponse<SessionModel>();
        
        try
        {
            var existingUser = await userManager.FindByNameAsync(loginDto.Username);

            if (existingUser == null || !await userManager.CheckPasswordAsync(existingUser, loginDto.Password))
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.Unauthorized;
                response.Message = "Invalid username or password";
                response.Errors = [ResponseError.InvalidCredentials];
                return response;
            }

            var userRoles = await userManager.GetRolesAsync(existingUser);
            var roleEnums = new List<AppRole>();

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                if (Enum.TryParse<AppRole>(userRole, out var parsedRole))
                {
                    roleEnums.Add(parsedRole);
                }
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            response.Data = new SessionModel
            {
                Username = existingUser.UserName,
                AccessToken = tokenHandler.WriteToken(token),
                Roles = roleEnums.ToList()
            };

            return response;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
            response.StatusCode = (HttpStatusCode)500;
        }
        return response;
    }

    public async Task<ApiResponse<SessionModel>> Register(RegisterDto registerDto)
    {
        var response = new ApiResponse<SessionModel>();

        try
        {
            var user = new User
            {
                UserName = registerDto.Username,
                RegisteredOn = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {

                await userManager.AddToRoleAsync(user, AppRole.User.GetDescription());
                response.Data = (await Login(registerDto)).Data;
            }
            else
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = result.Errors.FirstOrDefault().Description;
            }

            return response;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
            response.StatusCode = (HttpStatusCode)500;
        }
        return response;
    }
}