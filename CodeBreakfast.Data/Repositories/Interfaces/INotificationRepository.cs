using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Data.Repositories.Interfaces;

public interface INotificationRepository
{
    Task<List<Notification>> GetUserNotificationsAsync(Guid userId);
    Task SaveNotificationAsync(Notification notification);
    Task<Notification?> SaveNotificationForUserAsync(Notification notification, Guid userId);
    Task SaveNotificationForCourseAsync(Notification notification,Guid courseId);
    Task SaveNotificationForCourseAsync(Notification notification,Guid courseId, CourseRole courseRole);
    Task<bool> DeleteNotificationAsync(Guid notificationId);
}