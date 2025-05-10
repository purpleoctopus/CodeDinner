using System.ComponentModel;
using System.Reflection;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Common;

public static class CommonHelper
{

    #region Extensions
    
    // Models Extensions
    
    public static Course GetEntity(this CourseAddDto courseAddDto)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Name = courseAddDto.Name,
            Language = courseAddDto.Language,
            Description = courseAddDto.Description,
        };
    }
    public static Course GetEntity(this CourseUpdateDto courseUpdateDto)
    {
        return new Course
        {
            Id = courseUpdateDto.Id,
            Name = courseUpdateDto.Name,
            Language = courseUpdateDto.Language,
            Description = courseUpdateDto.Description
        };
    }
    public static Lesson GetEntity(this LessonAddDto lessonAddDto)
    {
        return new Lesson
        {
            Id = Guid.NewGuid(),
            Name = lessonAddDto.Name,
            Description = lessonAddDto.Description,
            LessonType = lessonAddDto.LessonType,
            CourseId = lessonAddDto.CourseId,
            HtmlContent = lessonAddDto.HtmlContent
        };
    }
    public static Lesson GetEntity(this LessonUpdateDto lessonUpdateDto)
    {
        return new Lesson
        {
            Id = lessonUpdateDto.Id,
            Name = lessonUpdateDto.Name,
            Description = lessonUpdateDto.Description,
            LessonType = lessonUpdateDto.LessonType,
            CourseId = lessonUpdateDto.CourseId,
            HtmlContent = lessonUpdateDto.HtmlContent
        };
    }

    public static UserActivity GetEntity(this UserActivityDetailDto userActivityDetailDto)
    {
        return new UserActivity
        {
            Id = userActivityDetailDto.Id,
            UserId = userActivityDetailDto.UserId,
            ActivityType = userActivityDetailDto.ActivityType,
            Title = userActivityDetailDto.Title,
            AdditionalJson = userActivityDetailDto.AdditionalJson
        };
    }
    
    public static CourseDetailDto GetCommonModel(this Course course)
    {
        return new CourseDetailDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
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
    public static LessonDetailDto GetCommonModel(this Lesson lesson)
    {
        return new LessonDetailDto
        {
            Id = lesson.Id,
            Name = lesson.Name,
            Description = lesson.Description,
            LessonType = lesson.LessonType,
            CourseId = lesson.CourseId,
            HtmlContent = lesson.HtmlContent,
            Duration = lesson.Duration
        };
    }
    public static UserActivityDetailDto GetCommonModel(this UserActivity userActivity)
    {
        return new UserActivityDetailDto
        {
            Id = userActivity.Id,
            UserId = userActivity.UserId,
            ActivityType = userActivity.ActivityType,
            Title = userActivity.Title,
            AdditionalJson = userActivity.AdditionalJson
        };
    }
    
    //Common Extensions

    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttribute<DescriptionAttribute>();
        return attr?.Description ?? value.ToString();
    }
    #endregion
}