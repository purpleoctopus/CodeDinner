using CodeBreakfast.Common;
using CodeBreakfast.DataLayer.Entities;
using CodeBreakfast.DataLayer.Enumerations;
using CodeBreakfast.Logic.Data;
using CodeBreakfast.Logic.Services.Interfaces;

namespace CodeBreakfast.Logic.Services;

public class EventService(INotificationService notificationService) : IEventService
{
    public async Task TriggerNewsletter(NewsletterEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendGlobalNotificationAsync(new NotificationData
        {
            NotificationType = NotificationType.Newsletter,
            Title = additionalData.Newsletter.Title,
            Description = additionalData.Newsletter.Content
        });
    }

    public async Task TriggerNewPrivateMessage(NewPrivateMessageEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendNotificationAsync(new NotificationData
        {
            NotificationType = NotificationType.NewPrivateMessage,
            Title = "New Message",
            Description = $"New message from {additionalData.Sender}",
            AdditionalData = additionalData
        }, additionalData.RecipientId);
    }

    public async Task TriggerProfileFollow(ProfileFollowEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendNotificationAsync(new NotificationData
        {
            NotificationType = NotificationType.ProfileFollow,
            Title = "Profile Follow",
            Description = "Someone followed to your profile",
            AdditionalData = additionalData
        }, additionalData.RecipientId);
    }

    public async Task TriggerCommentReply(CommentReplyEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendNotificationAsync(new NotificationData
        {
            NotificationType = NotificationType.CommentReply,
            Title = "User Replied",
            Description = $"{additionalData.Username} replied",
            AdditionalData = additionalData
        }, additionalData.RecipientId);
    }

    public async Task TriggerNewCourseContent(NewCourseContentEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendNotificationByCourseAsync(new NotificationData
        {
            NotificationType = NotificationType.NewCourseContent,
            Title = "New Course Content",
            Description = $"{additionalData.CourseName} got new content",
            AdditionalData = additionalData
        }, additionalData.CourseId);
    }

    public async Task TriggerCourseSubscribe(CourseSubscribeEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendNotificationByCourseAsync(new NotificationData
        {
            NotificationType = NotificationType.CourseSubscribe,
            Title = "Someone subscribed to your course",
            Description = $"{additionalData.Username} subsribed to your course",
            AdditionalData = additionalData
        }, additionalData.CourseId, CourseRole.Owner);
    }

    public async Task TriggerCourseComment(CourseCommentEventAdditionalData additionalData)
    {
        //TODO: Save to db
        
        await notificationService.SendNotificationByCourseAsync(new NotificationData
        {
            NotificationType = NotificationType.CourseComment,
            Title = "Someone commented to your course",
            Description = $"{additionalData.Username} commented to your course",
            AdditionalData = additionalData
        }, additionalData.CourseId, CourseRole.Owner);
    }
}