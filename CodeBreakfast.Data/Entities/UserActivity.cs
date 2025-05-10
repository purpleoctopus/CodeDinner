namespace CodeBreakfast.Data.Entities;

public class UserActivity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ActivityType ActivityType { get; set; }
    public string Title { get; set; }
    public string AdditionalJson { get; set; }
}