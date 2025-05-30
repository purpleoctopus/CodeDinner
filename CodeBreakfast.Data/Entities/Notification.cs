namespace CodeBreakfast.Data.Entities;

public class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public NotificationType NotificationType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public object? AdditionalData { get; set; }
}