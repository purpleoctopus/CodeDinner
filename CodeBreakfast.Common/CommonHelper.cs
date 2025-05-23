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
            PrimarySpecialization = courseAddDto.PrimarySpecialization,
            Tags = courseAddDto.Tags
        };
    }
    public static Course GetEntity(this CourseUpdateDto courseUpdateDto)
    {
        return new Course
        {
            Id = courseUpdateDto.Id,
            Name = courseUpdateDto.Name,
            Language = courseUpdateDto.Language,
            Description = courseUpdateDto.Description,
            PrimarySpecialization = courseUpdateDto.PrimarySpecialization,
            Tags = courseUpdateDto.Tags,
            IsVisible = courseUpdateDto.IsVisible
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
            HtmlContent = lessonAddDto.HtmlContent,
            ModuleId = lessonAddDto.ModuleId
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
            HtmlContent = lessonUpdateDto.HtmlContent,
            IsVisible = lessonUpdateDto.IsVisible,
            ModuleId = lessonUpdateDto.ModuleId
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
    public static UserConfig GetEntity(this UserConfigUpdateDto userConfigUpdateDto, Guid requestingUserId)
    {
        return new UserConfig
        {
            Id = Guid.NewGuid(),
            UserId = requestingUserId,
            Key = userConfigUpdateDto.Key,
            Value = userConfigUpdateDto.Value
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
            PrimarySpecialization = course.PrimarySpecialization,
            Tags = course.Tags.ToList(),
            Author = course.Author.GetCommonModel(),
            CreatedOn = course.CreatedOn,
            UpdatedOn = course.UpdatedOn
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
            Duration = lesson.Duration,
            ModuleId = lesson.ModuleId
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
    public static UserConfigDetailDto GetCommonModel(this UserConfig userConfig)
    {
        return new UserConfigDetailDto
        {
            UserId = userConfig.UserId,
            Key = userConfig.Key,
            Value = userConfig.Value
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