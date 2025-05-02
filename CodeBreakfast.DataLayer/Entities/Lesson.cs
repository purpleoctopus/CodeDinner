using CodeBreakfast.DataLayer.Entities.Abstractions;
namespace CodeBreakfast.DataLayer.Entities;

public class Lesson : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Enumerations.LessonType  LessonType { get; set; }
    public string Description { get; set; }
    public string? HtmlContent { get; set; }
    public TimeSpan? Duration { get; set; }
}