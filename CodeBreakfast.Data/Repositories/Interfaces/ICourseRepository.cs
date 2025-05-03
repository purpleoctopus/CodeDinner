using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(Guid id);
    Task<Course?> GetByIdAsync(Guid id, Guid userId);
    Task<Course?> AddAsync(Course course);
    Task<Course?> UpdateAsync(Course course);
    Task<bool> DeleteAsync(Guid id);
}