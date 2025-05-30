using CodeBreakfast.Data;

namespace CodeBreakfast.Logic.Data;

public class NotificationData
{
    public NotificationType NotificationType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public object? AdditionalData { get; set; }
}