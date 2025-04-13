using System.ComponentModel.DataAnnotations;
using CodeBreakfast.DataLayer.Enums;

namespace CodeBreakfast.DataLayer.Entities;

public class Course : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
    
    public ICollection<string> Modules { get; set; } = [];
}