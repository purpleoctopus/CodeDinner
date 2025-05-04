using CodeBreakfast.Logic.Data;

namespace CodeBreakfast.Logic.Services.Interfaces;

public interface IEventService
{
    Task TriggerNewsletter(NewsletterEventAdditionalData additionalData);
    Task TriggerNewPrivateMessage(NewPrivateMessageEventAdditionalData additionalData);
    Task TriggerProfileFollow(ProfileFollowEventAdditionalData additionalData);
    Task TriggerCommentReply(CommentReplyEventAdditionalData additionalData);
    Task TriggerNewCourseContent(NewCourseContentEventAdditionalData additionalData);
    
    Task TriggerCourseSubscribe(CourseSubscribeEventAdditionalData additionalData);
    Task TriggerCourseComment(CourseCommentEventAdditionalData additionalData);
}