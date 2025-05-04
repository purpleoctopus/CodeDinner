using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data.Repositories;

public class LessonRepository(AppDbContext dbContext) : ILessonRepository
{
    public async Task<Lesson?> GetLessonByIdAsync(Guid id)
    {
        return await dbContext.Lessons.SingleOrDefaultAsync(l => l.Id == id);
    }

    public async Task<List<Lesson>> GetLessonsForCourseAsync(Guid courseId)
    {
        return await dbContext.Lessons.Include(x=>x.Module).Where(x => x.CourseId == courseId).ToListAsync();
    }

    public async Task<Lesson> AddLessonAsync(Lesson lesson)
    {
        await dbContext.Lessons.AddAsync(lesson);
        return lesson;
    }

    public async Task<Lesson?> UpdateLessonAsync(Lesson lesson)
    {
        await dbContext.SaveChangesAsync();
        return lesson;
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