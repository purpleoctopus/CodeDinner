using CodeBreakfast.Common.Models;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<List<CourseDetailDto>>> GetAllForUserAsync(Guid userId); 
    Task<ApiResponse<List<CourseForListDto>>> Get_ForListViewAsync();
    Task<ApiResponse<CourseDetailDto>> GetByIdAsync(Guid id);
    Task<ApiResponse<CourseDetailDto>> GetByNameAsync(string courseName);
    Task<ApiResponse<CourseDetailDto>> AddAsync(CourseAddDto dto, Guid userId);
    Task<ApiResponse<CourseForListDto>> AccessCourse(Guid courseId, Guid userId);
    Task<ApiResponse<CourseDetailDto>> UpdateAsync(CourseUpdateDto dto);
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}