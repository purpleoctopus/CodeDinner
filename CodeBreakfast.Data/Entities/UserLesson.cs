namespace CodeBreakfast.Data.Entities;

public class UserLesson
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public Lesson Lesson { get; set; }
    public int? Progress { get; set; }
    public TimeSpan? VideoStoppedAt { get; set; }
    public DateTime SubscribedOn { get; set; }
    public DateTime LastActivity { get; set; }
    public string AdditionalJson { get; set; }
}