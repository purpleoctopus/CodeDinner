using CodeBreakfast.Common.Models;

namespace CodeBreakfast.Logic.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<SessionModel>> Login(LoginDto loginDto);
    Task<ApiResponse<SessionModel>> Register(RegisterDto registerDto);
}