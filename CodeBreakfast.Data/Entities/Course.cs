using System.ComponentModel.DataAnnotations;
using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class Course : UserCreatedEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
    
    public ICollection<Module> Modules { get; set; }
}