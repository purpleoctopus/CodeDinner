using System.Net;
using CodeBreakfast.Common;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Entities;
using CodeBreakfast.Logic.Interfaces;

namespace CodeBreakfast.Logic;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }
    public async Task<ApiResponse<Course>> AddAsync(AddCourseDto dto, Guid userId)
    {
        var response = new ApiResponse<Course>();
        
        try
        {
            var course = CommonHelper.Get_CourseFromDto(dto);
            
            course.CreatedOn = DateTime.Now;
            
            response.Data = await _courseRepository.AddAsync(course);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
    {
        var response = new ApiResponse<bool>();
        
        try
        {
            response.Data = await _courseRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<Course>> GetByIdAsync(Guid id)
    {
        var response = new ApiResponse<Course>();

        try
        {
            var data = await _courseRepository.GetByIdAsync(id);
            if (data == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            response.Data = data;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public Task<ApiResponse<Course>> GetByNameAsync(string courseName)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<Course>>> GetAllAsync()
    {
        var response = new ApiResponse<List<Course>>();

        try
        {
            var data = await _courseRepository.GetAllAsync();
            response.Data = data;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        return response;
    }

    public async Task<ApiResponse<Course>> UpdateAsync(UpdateCourseDto dto)
    {
        var response = new ApiResponse<Course>();

        try
        {
            var model = CommonHelper.Get_CourseFromDto(dto);
            var data = await _courseRepository.UpdateAsync(model);
            if (data == null)
            {
                response.Success = false;
                response.Message = "Course not found";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            response.Data = model;
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