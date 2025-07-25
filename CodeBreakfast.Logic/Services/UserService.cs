using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CodeBreakfast.Logic.Services;

public class UserService(IUserRepository userRepository, UserManager<User> userManager) : IUserService
{
    public async Task<ApiResponse<List<UserDetailDto>>> GetUsersList()
    {
        var response = new ApiResponse<List<UserDetailDto>>();

        try
        {
            var users = await userRepository.GetAllUsers();
            var result = new List<UserDetailDto>();
            
            foreach (var user in users)
            {
                var model = user.GetCommonModel();

                var roleStrings = await userManager.GetRolesAsync(user);

                var highestRole = roleStrings
                    .Select(roleStr => Enum.TryParse<AppRole>(roleStr, out var role) ? role : (AppRole?)null)
                    .Where(role => role != null)
                    .Select(role => role!.Value)
                    .DefaultIfEmpty(AppRole.User)
                    .Max();

                model.Role = highestRole;
                result.Add(model);
            }

            response.Data = result;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

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
        
        response.Data = new UserProfileDto
        {
            Id = user.Id,
            Username = user.UserName,
        };

        // Hide profile data when user profile is private
        if (requestingUserId != userId && await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.IsPrivate, userId))
        {
            response.Data.IsPrivate = true;
            return response;
        }
        
        var userCourses = await userRepository.GetUserCoursesForUserAsync(userId);
        
        var role = (await userManager.GetRolesAsync(user))
            .Select(Enum.Parse<AppRole>)
            .OrderByDescending(role => (int)role)
            .FirstOrDefault();

        if (await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.ViewLastActivity, userId))
        {
            response.Data.SectionsToView.Add(UserProfileSection.LastActivity);
        }

        if (await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.ViewCourseSummary, userId))
        {
            response.Data.SectionsToView.Add(UserProfileSection.CourseSummary);
        }
        
        if (await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.ViewCourseDetailStatistic, userId))
        {
            response.Data.SectionsToView.Add(UserProfileSection.CourseDetailStatistic);
        }
        
        response.Data.Role = role;
        response.Data.FirstName = user.FirstName;
        response.Data.LastName = user.LastName;
        response.Data.RegisteredOn = user.RegisteredOn;
        response.Data.CoursesCount = userCourses.Count;
        response.Data.CompletedCoursesCount = 0;
        response.Data.CreatedCoursesCount = userCourses.Count(x => x.Role == CourseRole.Owner);

        return response;
    }

    public async Task<ApiResponse<List<UserConfigDetailDto>>> GetUserConfiguration(Guid userId)
    {
        var response = new ApiResponse<List<UserConfigDetailDto>>();

        try
        {
            response.Data = (await userRepository.GetUserConfigsForUserAsync(userId)).Select(x=>x.GetCommonModel()).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

    public async Task<ApiResponse<byte[]>> GetUserProfilePicture(Guid requestingUserId, Guid userId)
    {
        var response = new ApiResponse<byte[]>();

        try
        {
            if (requestingUserId != userId && await userRepository.GetUserConfigValueByKeyAsync<bool>(UserConfigKey.IsPrivate, userId))
            {
                response.Success = false;
                response.Message = "No access";
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            
            var user = await userRepository.GetUserByIdAsync(userId);

            if (user?.ProfilePicture == null)
            {
                response.Success = false;
                response.Message = "No profile picture set";
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadsPath, user.ProfilePicture);

            if (!File.Exists(filePath))
            {
                response.Success = false;
                response.Message = "Profile picture file is deleted";
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }

            var bytes = await File.ReadAllBytesAsync(filePath);
            response.Data = bytes;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<UserCountStatisticsDto>> GetUserCountStatistics()
    {
        var response = new ApiResponse<UserCountStatisticsDto>();

        try
        {
            var users = await userRepository.GetAllUsers();
            
            var adminCount = 0;
            var moderatorCount = 0;
            var creatorCount = 0;

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Contains(AppRole.Admin.ToString())) adminCount++;
                if (roles.Contains(AppRole.Moderator.ToString())) moderatorCount++;
                if (roles.Contains(AppRole.Creator.ToString())) creatorCount++;
            }
            
            response.Data = new UserCountStatisticsDto
            {
                TotalCount = users.Count,
                AdminCount = adminCount,
                ModeratorCount = moderatorCount,
                CreatorCount = creatorCount
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

    public async Task<ApiResponse<List<UserConfigDetailDto>>> UpdateUserConfiguration(Guid userId, List<UserConfigUpdateDto> dto)
    {
        var response = new ApiResponse<List<UserConfigDetailDto>>();

        try
        {
            List<UserConfig> userConfigs = dto.Select(x=>x.GetEntity(userId)).ToList();
            response.Data = (await userRepository.UpdateUserConfigsAsync(userId, userConfigs)).Select(x=>x.GetCommonModel()).ToList();;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

    public async Task<ApiResponse<UserDetailDto>> UpdateUser(Guid requestingUserId, UserUpdateDto dto)
    {
        var response = new ApiResponse<UserDetailDto>();

        try
        {
            if (requestingUserId != dto.Id)
            {
                response.Success = false;
                response.Message = "ID Mismatch.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }
            
            var user = await userManager.FindByIdAsync(dto.Id.ToString());

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            
            user.UserName = dto.Username;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            await userManager.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

    public async Task<ApiResponse<bool>> UpdateUserRole(Guid userId, AppRole role)
    {
        var response = new ApiResponse<bool>();

        try
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                response.Success = false;
                response.Message = "User wasn't found.";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            
            var currentRoles = await userManager.GetRolesAsync(user);
            
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                response.Success = false;
                response.Message = "Error when cleaning roles.";
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
            
            var addResult = await userManager.AddToRoleAsync(user, role.ToString());
            if (!addResult.Succeeded)
            {
                response.Success = false;
                response.Message = "Error when assigning roles.";
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }
         
        return response;
    }

    public async Task<ApiResponse<bool>> UploadUserProfilePicture(Guid requestingUserId, IFormFile picture)
    {
        var response = new ApiResponse<bool>();

        try
        {
            if (picture == null || picture.Length == 0)
            {
                response.Success = false;
                response.Message = "File is empty";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);
            
            var fileExtension = Path.GetExtension(picture.FileName);

            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
            {
                response.Success = false;
                response.Message = "File is not a valid image file";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            
            var fileName = requestingUserId + fileExtension;
            var filePath = Path.Combine(uploadsPath, fileName);
            
            var user = await userManager.FindByIdAsync(requestingUserId.ToString());
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            if (user.ProfilePicture != null)
            {
                var oldFilePath = Path.Combine(uploadsPath, user.ProfilePicture);

                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }
            
            user.ProfilePicture = fileName;
            await userManager.UpdateAsync(user);
            
            response.Data = true;
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