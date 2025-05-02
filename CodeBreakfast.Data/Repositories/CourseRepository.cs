using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Entities;
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

    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await dbContext.Courses.FindAsync(id);

        if (model == null)
        {
            return false;
        }

        dbContext.Courses.Remove(model);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await dbContext.Courses.ToListAsync();
    }
    
    public async Task<List<Course>> GetAllForUserAsync(Guid userId)
    {
        return await dbContext.UserCourses
            .Include(x=>x.Course)
            .Where(x=>x.UserId == userId)
            .Select(x=>x.Course)
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await dbContext.Courses.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Course?> UpdateAsync(Course model)
    {
        var course = await dbContext.Courses.SingleOrDefaultAsync(x => x.Id == model.Id);
        
        if (course == null)
        {
            return null;
        }
        
        course.Name = model.Name;
        course.Language = model.Language;
        await dbContext.SaveChangesAsync();
        return course;
    }
}