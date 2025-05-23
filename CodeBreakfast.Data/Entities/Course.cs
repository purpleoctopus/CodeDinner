using System.ComponentModel.DataAnnotations;
using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class Course : UserCreatedEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public CourseLanguage Language { get; set; }
    public string? PrimarySpecialization { get; set; }
    public ICollection<string> Tags { get; set; } = new List<string>();
    public ICollection<Module> Modules { get; set; } = new List<Module>();
    public bool IsVisible { get; set; }
}