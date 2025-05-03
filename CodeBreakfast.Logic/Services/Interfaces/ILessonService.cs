using CodeBreakfast.Common.Models;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface ILessonService
{
    Task<ApiResponse<List<LessonDetailDto>>> GetAllForCourseAsync(Guid courseId, Guid requestingUserId);
    Task<ApiResponse<LessonDetailDto>> GetByIdAsync(Guid id, Guid requestingUserId);
    Task<ApiResponse<List<LessonForListDto>>> GetForListViewAsync(Guid courseId, Guid requestingUserId);
    Task<ApiResponse<LessonDetailDto>> AddAsync(LessonAddDto dto, Guid requestingUserId);
    Task<ApiResponse<LessonDetailDto>> UpdateAsync(LessonUpdateDto dto, Guid requestingUserId);
    Task<ApiResponse<bool>> DeleteAsync(Guid id, Guid requestingUserId);
}