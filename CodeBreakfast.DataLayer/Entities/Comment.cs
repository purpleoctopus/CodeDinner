using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBreakfast.DataLayer.Entities;

public class Comment
{
    public Guid Id { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [ForeignKey("Lessons")]
    public Guid LessonId { get; set; }
    public string Content { get; set; }
}