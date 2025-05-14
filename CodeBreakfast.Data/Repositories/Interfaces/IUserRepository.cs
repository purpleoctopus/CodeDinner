using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<List<Guid>> GetCourseUsersAsync(Guid courseId);
    Task<List<Guid>> GetCourseUsersAsync(Guid courseId, CourseRole courseRole);
    Task<List<Course>> GetCoursesForUserAsync(Guid userId);
    Task<List<UserCourse>> GetUserCoursesForUserAsync(Guid userId);
    Task<UserCourse?> GetUserCourseForUserAsync(Guid courseId, Guid userId);
    Task<List<UserCourse>> GetUserCoursesForCourseAsync(Guid courseId);
    Task<int> GetUsersCountForCourseAsync(Guid courseId);
    Task<UserCourse> CreateUserCourseAsync(UserCourse userCourse);
    
    //User Configs
    Task<T?> GetUserConfigValueByKeyAsync<T>(UserConfigKey key, Guid userId);
    Task<List<UserConfig>> GetUserConfigsForUserAsync(Guid userId);
    Task<List<UserConfig>> UpdateUserConfigsAsync(Guid courseId, List<UserConfig> userConfigs);
    
    //User Activities
    Task<List<UserActivity>> GetUserActivitiesForUserAsync(Guid userId);
    Task<UserActivity?> GetUserActivityAsync(Guid activityId);
    Task<UserActivity> CreateUserActivityAsync(UserActivity userActivity);
    Task<bool> DeleteUserActivityAsync(Guid activityId);
    Task<bool> DeleteAllUserActivitiesForUserAsync(Guid userId);
}