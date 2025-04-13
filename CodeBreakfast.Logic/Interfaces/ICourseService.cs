using CodeBreakfast.Common.Models;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Logic.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<List<Course>>> GetAllAsync(); 
    Task<ApiResponse<Course>> GetByIdAsync(Guid id);
    Task<ApiResponse<Course>> GetByNameAsync(string courseName);
    Task<ApiResponse<Course>> AddAsync(AddCourseDto dto, Guid userId);
    Task<ApiResponse<Course>> UpdateAsync(UpdateCourseDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}