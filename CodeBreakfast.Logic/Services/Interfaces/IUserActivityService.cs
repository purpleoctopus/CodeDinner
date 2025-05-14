using CodeBreakfast.Common.Models;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface IUserActivityService
{
    Task<ApiResponse<List<UserActivityDetailDto>>> GetUserActivityListAsync(Guid userId, Guid requestingUserId);
    Task<ApiResponse<UserActivityDetailDto>> CreateUserActivityAsync(UserActivityDetailDto userActivity, Guid userId);
    Task<ApiResponse<bool>> DeleteUserActivityAsync(Guid activityId, Guid userId);
    Task<ApiResponse<bool>> DeleteAllUserActivitiesForUserAsync(Guid userId);
}