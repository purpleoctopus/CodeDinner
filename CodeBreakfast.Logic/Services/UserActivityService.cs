using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;

namespace CodeBreakfast.Logic.Services;

public class UserActivityService(IUserRepository userRepository) : IUserActivityService
{
    public async Task<ApiResponse<List<UserActivityDetailDto>>> GetUserActivityListAsync(Guid userId, Guid requestingUserId)
    {
        var response = new ApiResponse<List<UserActivityDetailDto>>();

        try
        {
            if (userId != requestingUserId && await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.ViewLastActivity, userId) == false)
            {
                response.Success = false;
                response.Message = "User hide this section";
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            var userActivities = await userRepository.GetUserActivitiesForUserAsync(userId);
            response.Data = userActivities.Select(x=>x.GetCommonModel()).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
            return response;
        }

        return response;
    }

    public async Task<ApiResponse<UserActivityDetailDto>> CreateUserActivityAsync(UserActivityDetailDto userActivity, Guid userId)
    {
        var response = new ApiResponse<UserActivityDetailDto>();
        
        try
        {
            if (userActivity.UserId != userId)
            {
                response.Success = false;
                response.Message = "ID Mismatch.";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            var createdUserActivity = await userRepository.CreateUserActivityAsync(userActivity.GetEntity());
            response.Data = createdUserActivity.GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
            return response;
        }

        return response;
    }

    public async Task<ApiResponse<bool>> DeleteUserActivityAsync(Guid activityId, Guid userId)
    {
        var response = new ApiResponse<bool>();
        
        try
        {
            var existingActivity = await userRepository.GetUserActivityAsync(activityId);

            if (existingActivity == null)
            {
                response.Success = false;
                response.Message = "UserActivity doesn't exist";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            if (existingActivity.UserId != userId)
            {
                response.Success = false;
                response.Message = "No Access";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var deletionResult = await userRepository.DeleteUserActivityAsync(activityId);
            response.Data = deletionResult;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
            return response;
        }

        return response;
    }

    public async Task<ApiResponse<bool>> DeleteAllUserActivitiesForUserAsync(Guid userId)
    {
        var response = new ApiResponse<bool>();
        
        try
        {
            var deletionResult = await userRepository.DeleteAllUserActivitiesForUserAsync(userId);
            response.Data = deletionResult;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
            return response;
        }

        return response;
    }
}