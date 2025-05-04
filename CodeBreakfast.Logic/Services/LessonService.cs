using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                Duration = x.Duration
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

            var lesson = await lessonRepository.AddLessonAsync(dto.GetEntity());
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

            var lesson = await lessonRepository.UpdateLessonAsync(dto.GetEntity());
            if (lesson == null)
            {
                response.Success = false;
                response.Message = "No such lesson exists.";
                response.StatusCode = HttpStatusCode.NotFound;
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