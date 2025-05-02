using CodeBreakfast.DataLayer.Enumerations;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<Guid>> GetCourseUsersAsync(Guid courseId);
    Task<List<Guid>> GetCourseUsersAsync(Guid courseId, CourseRole courseRole);
}