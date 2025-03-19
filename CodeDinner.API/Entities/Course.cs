using System.ComponentModel.DataAnnotations;
using CodeDinner.API.Enums;

namespace CodeDinner.API.Entities;

public class Course
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
    
    public ICollection<Module> Modules { get; set; } = [];
}