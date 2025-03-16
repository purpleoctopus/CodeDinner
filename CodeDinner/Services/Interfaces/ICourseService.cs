using CodeDinner.Models.Domain;
using CodeDinner.Models.DTO;

namespace CodeDinner.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllAsync(); 
    Task<Course> GetByIdAsync(Guid id);
    Task<Course> GetByNameAsync(string courseName);
    Task CreateAsync(CreateCourseDto dto);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid id);
}