using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<List<UserDetailDto>>> GetUsersList();
    Task<ApiResponse<UserProfileDto>> GetUserProfileForView(Guid requestingUserId, Guid userId);
    Task<ApiResponse<List<UserConfigDetailDto>>> GetUserConfiguration(Guid userId);
    Task<ApiResponse<byte[]>> GetUserProfilePicture(Guid requestingUserId, Guid userId);
    Task<ApiResponse<UserCountStatisticsDto>> GetUserCountStatistics();
    Task<ApiResponse<List<UserConfigDetailDto>>> UpdateUserConfiguration(Guid userId, List<UserConfigUpdateDto> userConfigs);
    Task<ApiResponse<UserDetailDto>> UpdateUser(Guid requestingUserId, UserUpdateDto dto);
    Task<ApiResponse<bool>> UpdateUserRole(Guid userId, AppRole role);
    Task<ApiResponse<bool>> UploadUserProfilePicture(Guid requestingUserId, IFormFile picture);
}