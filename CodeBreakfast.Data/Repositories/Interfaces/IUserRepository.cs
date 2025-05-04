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
}