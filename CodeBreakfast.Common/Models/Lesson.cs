using CodeBreakfast.DataLayer.Enumerations;

namespace CodeBreakfast.Common.Models;

public class LessonAddDto
{
    public LessonType LessonType { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? HtmlContent { get; set; }
}

public class LessonUpdateDto : LessonAddDto
{
    public Guid Id { get; set; }
}

public class LessonDetailDto : LessonUpdateDto
{
    public TimeSpan? Duration { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}

public class LessonForListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeSpan? Duration { get; set; }
}