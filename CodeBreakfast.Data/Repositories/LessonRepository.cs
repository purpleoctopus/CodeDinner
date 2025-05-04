using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data.Repositories;

public class LessonRepository(AppDbContext dbContext) : ILessonRepository
{
    public async Task<Lesson?> GetLessonById(Guid id)
    {
        return await dbContext.Lessons.SingleOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lesson>> GetLessonsForCourseAsync(Guid courseId)
    {
        return await dbContext.Lessons.Where(x => x.CourseId == courseId).ToListAsync();
    }

    public async Task<Lesson> AddLessonAsync(Lesson lesson)
    {
        await dbContext.Lessons.AddAsync(lesson);
        return lesson;
    }

    public async Task<Lesson?> UpdateLessonAsync(Lesson lesson)
    {
        var existingLesson = await dbContext.Lessons.SingleOrDefaultAsync(x => x.Id == lesson.Id);
        if (existingLesson == null)
        {
            return null;
        }
        existingLesson.Name = lesson.Name;
        existingLesson.Description = lesson.Description;
        existingLesson.HtmlContent = lesson.HtmlContent;
        await dbContext.SaveChangesAsync();
        return existingLesson;
    }

    public async Task<bool> DeleteLessonAsync(Guid id)
    {
        var existingLesson = dbContext.Lessons.SingleOrDefault(l => l.Id == id);
        if (existingLesson == null)
        {
            return false;
        }
        dbContext.Lessons.Remove(existingLesson);
        await dbContext.SaveChangesAsync();
        return true;
    }
}