using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Logic.Data;

public class NewsletterEventAdditionalData
{
    public Newsletter Newsletter { get; set; }
}
public class NewPrivateMessageEventAdditionalData
{
    public Guid RecipientId { get; set; }
    public Guid ChatId { get; set; }
    public Guid MessageId { get; set; }
    public string Sender { get; set; }
}
public class ProfileFollowEventAdditionalData
{
    public Guid RecipientId { get; set; }
    public Guid FollowerId { get; set; }
}
public class CommentReplyEventAdditionalData
{
    public Guid RecipientId { get; set; }
    public Guid CourseId { get; set; }
    public Guid CommentId { get; set; }
    public string Username { get; set; }
    public string UserId { get; set; }
}
public class NewCourseContentEventAdditionalData
{
    public string CourseName { get; set; }
    public Guid CourseId { get; set; }
}
public class CourseSubscribeEventAdditionalData
{
    public string Username { get; set; }
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
}
public class CourseCommentEventAdditionalData
{
    public string Username { get; set; }
    public Guid CourseId { get; set; }
    public Guid CommentId { get; set; }
}