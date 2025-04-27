using CodeBreakfast.DataLayer.Enums;

namespace CodeBreakfast.DataLayer.Entities;

public class Lesson : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public LessonType  LessonType { get; set; }
    public string Description { get; set; }
    public string? HtmlContent { get; set; }
}