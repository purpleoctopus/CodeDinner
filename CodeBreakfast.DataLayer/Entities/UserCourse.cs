using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBreakfast.DataLayer.Entities;

public class UserCourse
{
    public int Id { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
    [ForeignKey("Courses")]
    public Guid  CourseId { get; set; }
    public Course Course { get; set; }
    public Review Review { get; set; }
}