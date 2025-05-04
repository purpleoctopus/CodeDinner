using CodeBreakfast.Data;
using CodeBreakfast.Logic.Data;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface INotificationService
{
    Task SendGlobalNotificationAsync(NotificationData data);
    Task SendNotificationAsync(NotificationData data, Guid userId);
    Task SendNotificationAsync(NotificationData data, List<Guid> userIds);
    Task SendNotificationByCourseAsync(NotificationData data, Guid courseId);
    Task SendNotificationByCourseAsync(NotificationData data, Guid courseId, CourseRole role);
    Task SendNotificationByCourseAsync(NotificationData data, Guid courseId, Guid userId);
}