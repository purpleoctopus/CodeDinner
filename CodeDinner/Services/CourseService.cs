using System.Net;
using CodeDinner.Exceptions;
using CodeDinner.Models.Domain;
using CodeDinner.Models.DTO;
using CodeDinner.Repositories.Interfaces;
using CodeDinner.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CodeDinner.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }
    public async Task CreateAsync(CreateCourseDto dto)
    {
        var model = new Course()
        {
            Name = dto.Name
        };
        try
        {
            await courseRepository.CreateAsync(model);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await courseRepository.DeleteAsync(id);
    }

    public async Task<Course> GetByIdAsync(Guid id)
    {
        var model = await courseRepository.GetByIdAsync(id);
        return model ?? throw new StatusCodeException(HttpStatusCode.NotFound);
    }

    public Task<Course> GetByNameAsync(string courseName)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await courseRepository.GetAllAsync();
    }

    public Task UpdateAsync(Course course)
    {
        throw new NotImplementedException();
    }
}