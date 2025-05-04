using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class Lesson : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Guid ModuleId { get; set; }
    public Module Module { get; set; }
    public LessonType  LessonType { get; set; }
    public string Description { get; set; }
    public string? HtmlContent { get; set; }
    public TimeSpan? Duration { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}