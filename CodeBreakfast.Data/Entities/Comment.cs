using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class Comment : UserCreatedEntity
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public string Content { get; set; }
}