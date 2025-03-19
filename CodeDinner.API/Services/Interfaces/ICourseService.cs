using CodeDinner.API.DTOs;
using CodeDinner.API.Entities;

namespace CodeDinner.API.Services.Interfaces;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllAsync(); 
    Task<Course> GetByIdAsync(Guid id);
    Task<Course> GetByNameAsync(string courseName);
    Task CreateAsync(AddCourseDto dto);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid id);
}