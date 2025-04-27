using CodeBreakfast.Common.Models;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Logic.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<List<CourseDetailDto>>> GetAllAsync(); 
    Task<ApiResponse<List<CourseForListDto>>> Get_ForListViewAsync();
    Task<ApiResponse<CourseDetailDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<CourseDetailDto>> GetByNameAsync(string courseName);
    Task<ApiResponse<CourseDetailDto>> AddAsync(CourseAddDto dto, Guid userId);
    Task<ApiResponse<CourseDetailDto>> UpdateAsync(CourseUpdateDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}