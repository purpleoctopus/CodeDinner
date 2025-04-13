using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CodeBreakfast.Logic;

public class UserService(IUserRepository userRepository, UserManager<User> userManager, IConfiguration configuration) : IUserService
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
                return response;
            }

            var userRoles = await userManager.GetRolesAsync(existingUser);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            response.Data = new SessionModel
            {
                AccessToken = tokenHandler.WriteToken(token),
                Roles = userRoles.ToList()
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
                UserName = registerDto.Username
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
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