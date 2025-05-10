namespace CodeBreakfast.Data.Entities;

public class UserCourse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public CourseRole Role { get; set; }
    public DateTime RegisteredOn { get; set; }
}