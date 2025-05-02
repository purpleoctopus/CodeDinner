using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Entities;
using CodeBreakfast.DataLayer.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<List<Guid>> GetCourseUsersAsync(Guid courseId)
    {
        var userIds = await dbContext.UserCourses
            .Where(x => x.CourseId == courseId)
            .Select(x => x.UserId)
            .ToListAsync();
        
        return userIds;
    }

    public async Task<List<Guid>> GetCourseUsersAsync(Guid courseId, CourseRole courseRole)
    {
        var userIds = await dbContext.UserCourses
            .Where(x => x.CourseId == courseId && x.Role == courseRole)
            .Select(x => x.UserId)
            .ToListAsync();
        
        return userIds;
    }
}