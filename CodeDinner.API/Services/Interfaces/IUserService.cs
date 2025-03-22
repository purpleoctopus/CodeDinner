using CodeDinner.API.Models;
using CodeDinner.API.Models.DTOs;

namespace CodeDinner.API.Services.Interfaces;

public interface IUserService
{
    Task<SessionModel> Login(LoginDto loginDto);
    Task<SessionModel> Register(RegisterDto registerDto);
}