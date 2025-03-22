using System.Net;
using CodeDinner.API.Data;
using CodeDinner.API.Entities;
using CodeDinner.API.Models;
using CodeDinner.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeDinner.API.Repositories.Implementation;

public class CourseRepository(AppDbContext context) : ICourseRepository
{
    public async Task<Course?> AddAsync(Course course)
    {
        await context.Courses.AddAsync(course);
        await context.SaveChangesAsync();
        return course;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var model = await context.Courses.FindAsync(id);

        if (model == null)
        {
            return false;
        }

        context.Courses.Remove(model);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await context.Courses.ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await context.Courses.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Course?> UpdateAsync(Course model)
    {
        var course = await context.Courses.SingleOrDefaultAsync(x => x.Id == model.Id);
        
        if (course == null)
        {
            return null;
        }
        
        course.Name = model.Name;
        course.Language = model.Language;
        course.Modules = model.Modules;
        await context.SaveChangesAsync();
        return course;
    }
}