using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<List<Course>> GetAllForUserAsync(Guid userId);
    Task<Course?> AddAsync(Course course);
    Task<Course?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<Course?> UpdateAsync(Course model);
}