using System.Net;
using CodeDinner.API.DTOs;
using CodeDinner.API.Entities;
using CodeDinner.API.Exceptions;
using CodeDinner.API.Repositories.Interfaces;
using CodeDinner.API.Services.Interfaces;

namespace CodeDinner.API.Services.Implementation;

public class CourseService : ICourseService
{
    private readonly ICourseRepository courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }
    public async Task CreateAsync(AddCourseDto pDto)
    {
        var model = DataMapping.CourseFromAddDto(pDto);
            
        try
        {
            await courseRepository.CreateAsync(model);
        }
        catch (Exception ex)
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