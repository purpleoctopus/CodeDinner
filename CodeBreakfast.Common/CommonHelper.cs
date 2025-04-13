using CodeBreakfast.Common.Models;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Common;

public static class CommonHelper
{
    
    // DTO to entity mapping
    public static Course Get_CourseFromDto(UpdateCourseDto updateDto)
    {
        return new Course
        {
            Id = updateDto.Id,
            Name = updateDto.Name,
            Language = updateDto.Language
        };
    }
    public static Course Get_CourseFromDto(AddCourseDto addDto)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Name = addDto.Name,
            Language = addDto.Language
        };
    }
}