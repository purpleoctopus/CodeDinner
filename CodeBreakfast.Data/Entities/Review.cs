using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class Review : UserCreatedEntity
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public int Rating { get; set; }
    public string Message { get; set; }
}