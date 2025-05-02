using CodeBreakfast.Data.Repositories.Interfaces;
using CodeBreakfast.DataLayer.Enumerations;
using CodeBreakfast.Logic.Data;
using CodeBreakfast.Logic.Hubs;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CodeBreakfast.Logic.Services;

public class NotificationService(IHubContext<NotificationHub> notificationHub, 
    IUserRepository userRepository,
    ConnectionManager connectionManager)
    : INotificationService
{
    private readonly ConnectionManager _connectionManager = connectionManager;

    public async Task SendGlobalNotificationAsync(NotificationData data)
    {
        await notificationHub.Clients.All.SendAsync("Notification", data);
    }

    public async Task SendNotificationAsync(NotificationData data, Guid userId)
    {
        var connectionId = connectionManager.GetConnectionIdByUserId(userId);
        await notificationHub.Clients.Client(connectionId).SendAsync("Notification", data);
    }

    public async Task SendNotificationAsync(NotificationData data, List<Guid> userIds)
    {
        foreach (var userId in userIds)
        {
            var connectionId = connectionManager.GetConnectionIdByUserId(userId);
            await notificationHub.Clients.Client(connectionId).SendAsync("Notification", data);
        }
    }

    public async Task SendNotificationByCourseAsync(NotificationData data, Guid courseId)
    {
        var userIds = await userRepository.GetCourseUsersAsync(courseId);
        
        data.AdditionalData = new
        {
            CourseId = courseId
        };

        foreach (var userId in userIds)
        {
            var connectionId = connectionManager.GetConnectionIdByUserId(userId);
            await notificationHub.Clients.Client(connectionId).SendAsync("Notification", data);
        }
    }

    public async Task SendNotificationByCourseAsync(NotificationData data, Guid courseId, CourseRole role)
    {
        var userIds = await userRepository.GetCourseUsersAsync(courseId, role);
        
        data.AdditionalData = new
        {
            CourseId = courseId
        };

        foreach (var userId in userIds)
        {
            var connectionId = connectionManager.GetConnectionIdByUserId(userId);
            await notificationHub.Clients.Client(connectionId).SendAsync("Notification", data);
        }
    }

    public async Task SendNotificationByCourseAsync(NotificationData data, Guid courseId, Guid userId)
    {
        var userIds = await userRepository.GetCourseUsersAsync(courseId);

        if(userIds.Contains(userId)){
            var connectionId = connectionManager.GetConnectionIdByUserId(userId);
            
            await notificationHub.Clients.Client(connectionId).SendAsync("Notification", data);
        }
    }
}