using CodeDinner.API.Entities;

namespace CodeDinner.API.Repositories.Interfaces;

public interface ICourseRepository
{
    Task CreateAsync(Course course);
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Course course);
}