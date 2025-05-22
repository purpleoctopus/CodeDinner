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
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadsPath, user.ProfilePicture);

            if (!File.Exists(filePath))
            {
                response.Success = false;
                response.Message = "No profile picture set";
                response.StatusCode = HttpStatusCode.NotFound;
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

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);
            
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