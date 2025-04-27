namespace CodeBreakfast.DataLayer.Entities;

public class UserLesson
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public Lesson Lesson { get; set; }
    public int? Progress { get; set; }
    public TimeSpan? VideoStoppedAt { get; set; }
    public string AdditionalJson { get; set; }
}