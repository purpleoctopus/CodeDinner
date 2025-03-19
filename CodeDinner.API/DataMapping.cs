using CodeDinner.API.DTOs;
using CodeDinner.API.Entities;

namespace CodeDinner.API;

public static class DataMapping
{
    public static Course CourseFromAddDto(AddCourseDto pAddCourseDto)
    {
        return new Course()
        {
            Name = pAddCourseDto.Name,
            Language = pAddCourseDto.Language,
        };
    }
}