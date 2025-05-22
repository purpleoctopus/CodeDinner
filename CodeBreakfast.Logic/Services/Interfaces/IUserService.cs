using CodeBreakfast.Common.Models;
using CodeBreakfast.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserProfileDto>> GetUserProfileForView(Guid requestingUserId, Guid userId);
    Task<ApiResponse<List<UserConfigDetailDto>>> GetUserConfiguration(Guid userId);
    Task<ApiResponse<byte[]>> GetUserProfilePicture(Guid requestingUserId, Guid userId);
    Task<ApiResponse<List<UserConfigDetailDto>>> UpdateUserConfiguration(Guid userId, List<UserConfigUpdateDto> userConfigs);
    Task<ApiResponse<UserDetailDto>> UpdateUser(Guid requestingUserId, UserUpdateDto dto);
    Task<ApiResponse<bool>> UploadUserProfilePicture(Guid requestingUserId, IFormFile picture);
}