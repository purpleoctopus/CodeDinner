using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBreakfast.DataLayer.Entities;

public class Review
{
    public Guid Id { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
    [ForeignKey("Courses")]
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public int Rating { get; set; }
    public string Message { get; set; }
}