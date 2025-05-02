using System.ComponentModel.DataAnnotations.Schema;
using CodeBreakfast.DataLayer.Entities.Abstractions;

namespace CodeBreakfast.DataLayer.Entities;

public class Comment : UserCreatedEntity
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public string Content { get; set; }
}