using CodeDinner.API.Entities;
using CodeDinner.API.Models.DTOs;
using CodeDinner.API.Models;

namespace CodeDinner.API.Services.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<List<Course>>> GetAllAsync(); 
    Task<ApiResponse<Course>> GetByIdAsync(Guid id);
    Task<ApiResponse<Course>> GetByNameAsync(string courseName);
    Task<ApiResponse<Course>> AddAsync(AddCourseDto dto);
    Task<ApiResponse<Course>> UpdateAsync(UpdateCourseDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}