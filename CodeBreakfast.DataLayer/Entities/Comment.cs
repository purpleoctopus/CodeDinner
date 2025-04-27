namespace CodeBreakfast.DataLayer.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid LessonId { get; set; }
    public string Content { get; set; }
}