using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data.Repositories;

public class CourseRepository(AppDbContext dbContext) : ICourseRepository
{
    public async Task<Course?> AddAsync(Course course)
    {
        await dbContext.Courses.AddAsync(course);
        await dbContext.SaveChangesAsync();
        return course;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await dbContext.Courses.ToListAsync();
    }

    public async Task<List<Course>> GetAllByAuthorAsync(Guid userId)
    {
        return await dbContext.Courses.Where(x => x.AuthorId == userId).ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await dbContext.Courses.SingleOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Course?> GetByIdAsync(Guid id, Guid userId)
    {
        return await dbContext.UserCourses.Include(x=>x.Course)
            .Where(x=>x.UserId == userId && x.CourseId == id)
            .Select(x=>x.Course).SingleOrDefaultAsync();
    }

    public async Task<Course?> UpdateAsync(Course course)
    {
        await dbContext.SaveChangesAsync();
        return course;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingCourse = await dbContext.Courses.FindAsync(id);

        if (existingCourse == null)
        {
            return false;
        }

        dbContext.Courses.Remove(existingCourse);
        await dbContext.SaveChangesAsync();
        return true;
    }
}