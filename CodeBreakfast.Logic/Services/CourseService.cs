using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;

namespace CodeBreakfast.Logic.Services;

public class CourseService(ICourseRepository courseRepository, IUserRepository userRepository,
    ISecurityService securityService, ILessonRepository lessonRepository)
    : ICourseService
{
    #region Get Methods
    
    public async Task<ApiResponse<List<CourseDetailDto>>> GetAllForUserAsync(Guid requestingUserId)
    {
        var response = new ApiResponse<List<CourseDetailDto>>();

        try
        {
            var data = await userRepository.GetCoursesForUserAsync(requestingUserId);
            response.Data = data.Select(x=>x.GetCommonModel()).ToList();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<List<CourseForListDto>>> GetForListViewAsync()
    {
        var response = new ApiResponse<List<CourseForListDto>>();
        var coursesForList = new List<CourseForListDto>();

        try
        {
            var courses = (await courseRepository.GetAllAsync()).Where(x=>x.IsVisible).ToList();
            foreach (var course in courses)
            {
                var associatedLessons = await lessonRepository.GetLessonsForCourseAsync(course.Id);
                
                var courseForView = new CourseForListDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Language = course.Language,
                    ModulesCount = course.Modules.Count,
                    StudentsCount = await userRepository.GetUsersCountForCourseAsync(course.Id),
                    LessonsCount = associatedLessons.Where(x=>x.CourseId == course.Id).Count(),
                    TotalTime = TimeSpan.FromMinutes(associatedLessons.Select(l=>l.Duration?.TotalMinutes).Sum() ?? 0),
                    Author = (await userRepository.GetUserByIdAsync(course.AuthorId)).GetCommonModel(),
                    CreatedOn = course.CreatedOn,
                    UpdatedOn = course.UpdatedOn
                };
                
                coursesForList.Add(courseForView);
            }
            response.Data = coursesForList;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<CourseDetailDto>> GetByIdAsync(Guid id, Guid requestingUserId)
    {
        var response = new ApiResponse<CourseDetailDto>();

        try
        {
            var data = await courseRepository.GetByIdAsync(id, requestingUserId);
            if (data == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            if (!data.IsVisible && data.AuthorId != requestingUserId)
            {
                response.Success = false;
                response.Message = "Course is not verified";
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            response.Data = data.GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public Task<ApiResponse<CourseDetailDto>> GetByNameAsync(string courseName)
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Add Methods
    
    public async Task<ApiResponse<CourseDetailDto>> AddAsync(CourseAddDto dto, Guid requestingUserId)
    {
        var response = new ApiResponse<CourseDetailDto>();
        
        try
        {
            var course = dto.GetEntity();

            course.AuthorId = requestingUserId;
            course.CreatedOn = DateTime.UtcNow;
            course.UpdatedOn = DateTime.UtcNow;
            
            response.Data = (await courseRepository.AddAsync(course)).GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    #endregion
    
    #region Update Methods
    
    public async Task<ApiResponse<CourseForListDto>> AccessCourse(Guid courseId, Guid requestingUserId)
    {
        var response = new ApiResponse<CourseForListDto>();

        try
        {
            var userCourse = await userRepository.GetUserCourseForUserAsync(courseId, requestingUserId);
            if (userCourse == null)
            {
                await userRepository.CreateUserCourseAsync(new UserCourse
                {
                    CourseId = courseId, 
                    UserId = requestingUserId
                });
            }
            else
            {
                response.Success = false;
                response.Message = "You are already access this course";
                response.StatusCode = HttpStatusCode.Forbidden;
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

    public async Task<ApiResponse<CourseDetailDto>> UpdateAsync(CourseUpdateDto dto, Guid requestingUserId)
    {
        var response = new ApiResponse<CourseDetailDto>();

        try
        {
            if (await securityService.CourseAccessLevel(dto.Id, requestingUserId) != SectionAccess.Manage)
            {
                response.Success = false;
                response.Message = "No access";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }
            
            var existingCourse = await courseRepository.GetByIdAsync(dto.Id, requestingUserId);
            
            if (existingCourse == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            
            existingCourse.UpdatedOn = DateTime.UtcNow;
            existingCourse.Name = dto.Name;
            existingCourse.Language = dto.Language;
            existingCourse.Description = dto.Description;
            existingCourse.Modules = dto.Modules ?? [];
            existingCourse.IsVisible = dto.IsVisible;
            
            await courseRepository.UpdateAsync(existingCourse);
            response.Data = existingCourse.GetCommonModel();
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }
    
    #endregion
    
    #region Delete Methods
    
    public async Task<ApiResponse<bool>> DeleteAsync(Guid id, Guid requestingUserId)
    {
        var response = new ApiResponse<bool>();
        
        try
        {
            if (await securityService.CourseAccessLevel(id, requestingUserId) != SectionAccess.Manage)
            {
                response.Success = false;
                response.Message = "No access";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }
            response.Data = await courseRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }
    
    #endregion
}