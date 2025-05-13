using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;

namespace CodeBreakfast.Logic.Services;

public class LessonService(ILessonRepository lessonRepository, ISecurityService securityService)
    : ILessonService
{
    public async Task<ApiResponse<List<LessonDetailDto>>> GetAllForCourseAsync(Guid courseId, Guid requestingUserId)
    {
        var response = new ApiResponse<List<LessonDetailDto>>();

        try
        {
            if (await securityService.CourseAccessLevel(requestingUserId, courseId) == SectionAccess.None)
            {
                response.Success = false;
                response.Message = "You do not have permission to access this course.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }

            var lessons = await lessonRepository.GetLessonsForCourseAsync(courseId);
            response.Data = lessons.Select(x => x.GetCommonModel()).ToList();
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

    public async Task<ApiResponse<LessonDetailDto>> GetByIdAsync(Guid id, Guid requestingUserId)
    {
        var response = new ApiResponse<LessonDetailDto>();

        try
        {
            var lesson = await lessonRepository.GetLessonByIdAsync(id);
            
            if (lesson == null)
            {
                response.Success = false;
                response.Message = "No such lesson exists.";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            if (await securityService.CourseAccessLevel(requestingUserId, lesson.CourseId) == SectionAccess.None)
            {
                response.Success = false;
                response.Message = "You do not have permission to access this course.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }

            response.Data = lesson.GetCommonModel();
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

    public async Task<ApiResponse<List<LessonForListDto>>> GetForListViewAsync(Guid courseId, Guid requestingUserId)
    {
        var response = new ApiResponse<List<LessonForListDto>>();

        try
        {
            if (await securityService.CourseAccessLevel(requestingUserId, courseId) == SectionAccess.None)
            {
                response.Success = false;
                response.Message = "You do not have permission to access this course.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }

            var lessons = await lessonRepository.GetLessonsForCourseAsync(courseId);
            response.Data = lessons.Select(x => new LessonForListDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Duration = x.Duration,
                ModuleId = x.ModuleId
            }).ToList();
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

    public async Task<ApiResponse<LessonDetailDto>> AddAsync(LessonAddDto dto, Guid requestingUserId)
    {
        var response = new ApiResponse<LessonDetailDto>();

        try
        {
            if (await securityService.CourseAccessLevel(requestingUserId, dto.CourseId) == SectionAccess.None)
            {
                response.Success = false;
                response.Message = "You do not have permission to access this course.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }

            var lesson = dto.GetEntity();

            lesson.AuthorId = requestingUserId;
            lesson.CreatedOn = DateTime.Now;
            lesson.UpdatedOn = DateTime.Now;
            
            response.Data = (await lessonRepository.AddLessonAsync(lesson)).GetCommonModel();
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

    public async Task<ApiResponse<LessonDetailDto>> UpdateAsync(LessonUpdateDto dto, Guid requestingUserId)
    {
        var response = new ApiResponse<LessonDetailDto>();

        try
        {
            if (await securityService.CourseAccessLevel(requestingUserId, dto.CourseId) == SectionAccess.None)
            {
                response.Success = false;
                response.Message = "You do not have permission to access this course.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }

            var existingLesson = await lessonRepository.GetLessonByIdAsync(dto.Id);
            if (existingLesson == null)
            {
                response.Success = false;
                response.Message = "No such lesson exists.";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            existingLesson.UpdatedOn = DateTime.Now;
            existingLesson.Name = dto.Name;
            existingLesson.Description = dto.Description;
            existingLesson.HtmlContent = dto.HtmlContent;
            existingLesson.IsVisible = dto.IsVisible;

            var updatedLesson = await lessonRepository.UpdateLessonAsync(existingLesson);
            response.Data = updatedLesson.GetCommonModel();
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

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id, Guid requestingUserId)
    {
        var response = new ApiResponse<bool>();

        try
        {
            var existingLesson = await lessonRepository.GetLessonByIdAsync(id);
            
            if (existingLesson == null)
            {
                response.Success = false;
                response.Message = "No such lesson exists.";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            if (await securityService.CourseAccessLevel(requestingUserId, existingLesson.CourseId) == SectionAccess.None)
            {
                response.Success = false;
                response.Message = "You do not have permission to access this course.";
                response.StatusCode = HttpStatusCode.Forbidden;
                return response;
            }

            var deletionResult = await lessonRepository.DeleteLessonAsync(id);
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