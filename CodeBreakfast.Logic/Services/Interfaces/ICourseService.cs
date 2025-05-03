using CodeBreakfast.Common.Models;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface ICourseService
{
    Task<ApiResponse<List<CourseDetailDto>>> GetAllForUserAsync(Guid requestingUserId); 
    Task<ApiResponse<List<CourseForListDto>>> GetForListViewAsync();
    Task<ApiResponse<CourseDetailDto>> GetByIdAsync(Guid id, Guid requestingUserId);
    Task<ApiResponse<CourseDetailDto>> GetByNameAsync(string courseName);
    Task<ApiResponse<CourseDetailDto>> AddAsync(CourseAddDto dto, Guid requestingUserId);
    Task<ApiResponse<CourseForListDto>> AccessCourse(Guid courseId, Guid requestingUserId);
    Task<ApiResponse<CourseDetailDto>> UpdateAsync(CourseUpdateDto dto, Guid requestingUserId);
    Task<ApiResponse<bool>> DeleteAsync(Guid id, Guid requestingUserId);
}