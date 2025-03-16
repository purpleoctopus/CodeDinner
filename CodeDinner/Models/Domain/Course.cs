using System.ComponentModel.DataAnnotations;

namespace CodeDinner.Models.Domain;

public class Course
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<Module> Modules { get; set; } = [];
}