namespace CodeDinner.Models.Domain;

public class Module
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
}