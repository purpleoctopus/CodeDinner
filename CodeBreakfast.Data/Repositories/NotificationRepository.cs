using CodeBreakfast.Data.Entities;
using CodeBreakfast.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeBreakfast.Data.Repositories;

public class NotificationRepository(AppDbContext dbContext) : INotificationRepository
{
    public async Task<List<Notification>> GetUserNotificationsAsync(Guid userId)
    {
        return await dbContext.Notifications.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Notification?> SaveNotificationForUserAsync(Notification notification, Guid userId)
    {
        notification.UserId = userId;
        await dbContext.Notifications.AddAsync(notification);
        await dbContext.SaveChangesAsync();
        return notification;
    }

    public async Task SaveNotificationForCourseAsync(Notification notification, Guid courseId)
    {
        foreach (var userCourse in await dbContext.UserCourses.Where(x => x.CourseId == courseId).ToListAsync())
        {
            notification.UserId = userCourse.UserId;
            await dbContext.Notifications.AddAsync(notification);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveNotificationForCourseAsync(Notification notification, Guid courseId, CourseRole courseRole)
    {
        foreach (var userCourse in await dbContext.UserCourses
                     .Where(x => x.CourseId == courseId && x.Role == courseRole).ToListAsync())
        {
            notification.UserId = userCourse.UserId;
            await dbContext.Notifications.AddAsync(notification);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveNotificationAsync(Notification notification)
    {
        foreach (var user in await dbContext.Users.ToListAsync())
        {
            notification.UserId = user.Id;
            await dbContext.Notifications.AddAsync(notification);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteNotificationAsync(Guid notificationId)
    {
        var existingNotification = await dbContext.Notifications.FindAsync(notificationId);

        if (existingNotification == null)
        {
            return false;
        }
        
        dbContext.Notifications.Remove(existingNotification);
        await dbContext.SaveChangesAsync();
        return true;
    }
}