using CodeBreakfast.Common.Models;
using CodeBreakfast.DataLayer.Entities;

namespace CodeBreakfast.Common;

public static class CommonHelper
{

    #region Extensions
    
    public static Course GetEntity(this CourseUpdateDto courseUpdateDto)
    {
        return new Course
        {
            Id = courseUpdateDto.Id,
            Name = courseUpdateDto.Name,
            Language = courseUpdateDto.Language
        };
    }
    public static Course GetEntity(this CourseAddDto courseAddDto)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Name = courseAddDto.Name,
            Language = courseAddDto.Language
        };
    }
    
    public static CourseDetailDto GetCommonModel(this Course course)
    {
        return new CourseDetailDto
        {
            Id = course.Id,
            Name = course.Name,
            Language = course.Language,
            Modules = course.Modules.ToList(),
            Author = course.Author.GetCommonModel(),
            CreatedOn = course.CreatedOn,
            UpdatedOn = course.UpdatedOn
        };
    }
    
    public static UserDetailDto GetCommonModel(this User userDto)
    {
        return new UserDetailDto
        {
            Id = Guid.NewGuid(),
            Username = userDto.UserName,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };
    }
    
    #endregion
}