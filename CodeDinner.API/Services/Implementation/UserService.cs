using CodeDinner.API.Entities;
using CodeDinner.API.Models;
using CodeDinner.API.Models.DTOs;
using CodeDinner.API.Repositories.Interfaces;
using CodeDinner.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CodeDinner.API.Services.Implementation;

public class UserService(IUserRepository userRepository, UserManager<User> userManager) : IUserService
{
    public async Task<SessionModel> Login(LoginDto loginDto)
    {
        var existingUser = await userManager.FindByNameAsync(loginDto.Username);

        if (existingUser == null || !await userManager.CheckPasswordAsync(existingUser, loginDto.Password))
        {
            
        }

        return new SessionModel
        {
            
        };
    }

    public Task<SessionModel> Register(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }
}