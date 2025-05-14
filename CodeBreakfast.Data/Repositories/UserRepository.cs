using System.Text.Json;
using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid userId)
    {
        return dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
    }

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
    
    public async Task<List<Course>> GetCoursesForUserAsync(Guid userId)
    {
        return await dbContext.UserCourses
            .Include(x=>x.Course)
            .Where(x=>x.UserId == userId)
            .Select(x=>x.Course)
            .ToListAsync();
    }

    public async Task<List<UserCourse>> GetUserCoursesForUserAsync(Guid userId)
    {
        return await dbContext.UserCourses.Where(x => x.UserId == userId)
            .AsNoTracking().ToListAsync();
    }

    public async Task<UserCourse?> GetUserCourseForUserAsync(Guid courseId, Guid userId)
    {
        return await dbContext.UserCourses
            .Where(x => x.UserId == userId && x.CourseId == courseId)
            .AsNoTracking().SingleOrDefaultAsync();
    }

    public Task<List<UserCourse>> GetUserCoursesForCourseAsync(Guid courseId)
    {
        return dbContext.UserCourses.Where(x => x.CourseId == courseId).ToListAsync();
    }

    public async Task<int> GetUsersCountForCourseAsync(Guid courseId)
    {
        return await dbContext.UserCourses.CountAsync(x => x.CourseId == courseId);
    }

    public async Task<UserCourse> CreateUserCourseAsync(UserCourse userCourse)
    {
        await dbContext.UserCourses.AddAsync(userCourse);
        await dbContext.SaveChangesAsync();
        return userCourse;
    }
    
    //User Configs Related

    public async Task<T?> GetUserConfigValueByKeyAsync<T>(UserConfigKey key, Guid userId)
    {
        var valueString =
            (await dbContext.UserConfigs.AsNoTracking().FirstOrDefaultAsync(x => x.Key == key && x.UserId == userId))?.Value 
            ?? GetDefaultUserConfigValue(key);

        return valueString == null ? default : JsonSerializer.Deserialize<T>(valueString);
    }

    public async Task<List<UserConfig>> GetUserConfigsForUserAsync(Guid userId)
    {
        var userConfigs = await dbContext.UserConfigs.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var defaultUserConfigs = Enum.GetValues(typeof(UserConfigKey))
            .Cast<UserConfigKey>()
            .Select(key => new UserConfig
            {
                UserId = userId,
                Key = key,
                Value = GetDefaultUserConfigValue(key)
            })
            .ToDictionary(x => x.Key);
        
        foreach (var config in userConfigs)
        {
            defaultUserConfigs[config.Key] = config;
        }

        return defaultUserConfigs.Values.ToList();
    }

    public async Task<List<UserConfig>> UpdateUserConfigsAsync(Guid courseId, List<UserConfig> userConfigs)
    {
        var uniqueUserConfigs = userConfigs
            .GroupBy(x => x.Key)
            .Select(g => g.First())
            .ToList();

        var existingUserConfigs = await dbContext.UserConfigs
            .Where(x => x.UserId == courseId)
            .ToListAsync();

        var existingConfigMap = existingUserConfigs.ToDictionary(x => x.Key);

        foreach (var config in uniqueUserConfigs)
        {
            if (existingConfigMap.TryGetValue(config.Key, out var existingConfig))
            {
                existingConfig.Value = config.Value;
                dbContext.UserConfigs.Update(existingConfig);
            }
            else
            {
                dbContext.UserConfigs.Add(config);
            }
        }

        await dbContext.SaveChangesAsync();

        return await dbContext.UserConfigs.Where(x => x.UserId == courseId).ToListAsync();
    }

    //User Activities Related

    public async Task<List<UserActivity>> GetUserActivitiesForUserAsync(Guid userId)
    {
        return await dbContext.UserActivities.Where(a => a.UserId == userId).ToListAsync();
    }

    public async Task<UserActivity?> GetUserActivityAsync(Guid activityId)
    {
        return await dbContext.UserActivities.SingleOrDefaultAsync(x=>x.Id == activityId);
    }

    public async Task<UserActivity> CreateUserActivityAsync(UserActivity userActivity)
    {
        await dbContext.UserActivities.AddAsync(userActivity);
        await dbContext.SaveChangesAsync();
        return userActivity;
    }

    public async Task<bool> DeleteUserActivityAsync(Guid activityId)
    {
        var userActivity = await dbContext.UserActivities.SingleOrDefaultAsync(x => x.UserId == activityId);

        if (userActivity == null)
        {
            return false;
        }
        
        dbContext.UserActivities.Remove(userActivity);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAllUserActivitiesForUserAsync(Guid userId)
    {
        var deletedActivitiesCount = await dbContext.UserActivities.Where(x=>x.UserId == userId).ExecuteDeleteAsync();

        return deletedActivitiesCount != 0;
    }
    
    private static string? GetDefaultUserConfigValue(UserConfigKey configKey)
    {
        string? value = null;
        switch (configKey)
        {
            case UserConfigKey.IsPrivate:
                value = "true";
                break;
            case UserConfigKey.ViewCourseSummary:
                value = "true";
                break;
            case UserConfigKey.ViewCourseDetailStatistic:
                value = "true";
                break;
            case UserConfigKey.ViewLastActivity:
                value = "true";
                break;
        }

        return value;   
    }
}