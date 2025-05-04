using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBreakfast.Data.Entities.Abstractions;

public abstract class UserCreatedEntity
{
    [ForeignKey("User")]
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}