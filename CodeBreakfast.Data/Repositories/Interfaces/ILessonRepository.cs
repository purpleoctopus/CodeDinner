using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface ILessonRepository
{
    Task<Lesson?> GetLessonByIdAsync(Guid id);
    Task<List<Lesson>> GetLessonsForCourseAsync(Guid courseId);
    Task<Lesson> AddLessonAsync(Lesson lesson);
    Task<Lesson?> UpdateLessonAsync(Lesson lesson);
    Task<bool> DeleteLessonAsync(Guid id);
}