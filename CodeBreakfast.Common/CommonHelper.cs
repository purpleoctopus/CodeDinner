using CodeBreakfast.Common.Models;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Common;

public static class CommonHelper
{
    
    // DTO to entity mapping
    public static Course Get_CourseFromDto(CourseUpdateDto courseUpdateDto)
    {
        return new Course
        {
            Id = courseUpdateDto.Id,
            Name = courseUpdateDto.Name,
            Language = courseUpdateDto.Language
        };
    }
    public static Course Get_CourseFromDto(CourseAddDto courseAddDto)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Name = courseAddDto.Name,
            Language = courseAddDto.Language
        };
    }
}