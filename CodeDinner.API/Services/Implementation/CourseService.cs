using System.Net;
using Azure;
using CodeDinner.API.Entities;
using CodeDinner.API.Models;
using CodeDinner.API.Models.DTOs;
using CodeDinner.API.Repositories.Interfaces;
using CodeDinner.API.Services.Interfaces;

namespace CodeDinner.API.Services.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        this._courseRepository = courseRepository;
    }
    public async Task<ApiResponse<Course>> AddAsync(AddCourseDto dto)
    {
        var response = new ApiResponse<Course>();
        
        try
        {
            var model = new Course
            {
                Name = dto.Name,
                Language = dto.Language
            };
            response.Data = await _courseRepository.AddAsync(model);
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
            var model = new Course
            {
                Id = dto.Id,
                Name = dto.Name,
                Language = dto.Language,
                Modules = dto.Modules
            };
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