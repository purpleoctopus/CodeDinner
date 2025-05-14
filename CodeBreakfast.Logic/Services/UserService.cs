using System.Net;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CodeBreakfast.Logic.Services;

public class UserService(IUserRepository userRepository, UserManager<User> userManager) : IUserService
{
    public async Task<ApiResponse<UserProfileDto>> GetUserProfileForView(Guid requestingUserId, Guid userId)
    {
        var response = new ApiResponse<UserProfileDto>();
        
        var user = await userRepository.GetUserByIdAsync(userId);

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found";
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        // Hide profile data when user profile is private
        if (requestingUserId != userId && await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.IsPrivate, userId))
        {
            response.Data = new UserProfileDto
            {
                Id = user.Id,
                Username = user.UserName,
                IsPrivate = true
            };
            return response;
        }
        
        var userCourses = await userRepository.GetUserCoursesForUserAsync(userId);
        
        var role = (await userManager.GetRolesAsync(user))
            .Select(Enum.Parse<AppRole>)
            .OrderByDescending(role => (int)role)
            .FirstOrDefault();

        response.Data = new UserProfileDto
        {
            Id = user.Id,
            Role = role,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RegisteredOn = user.RegisteredOn,
            CoursesCount = userCourses.Count,
            CompletedCoursesCount = 0,
            CreatedCoursesCount = userCourses.Count(x => x.Role == CourseRole.Owner)
        };

        return response;
    }

    public async Task<ApiResponse<List<UserConfig>>> GetUserConfiguration(Guid userId)
    {
        var response = new ApiResponse<List<UserConfig>>();

        try
        {
            response.Data = await userRepository.GetUserConfigsForUserAsync(userId);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

    public async Task<ApiResponse<List<UserConfig>>> UpdateUserConfiguration(Guid userId, List<UserConfig> userConfigs)
    {
        var response = new ApiResponse<List<UserConfig>>();

        try
        {
            response.Data = await userRepository.UpdateUserConfigsAsync(userId, userConfigs);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }
}