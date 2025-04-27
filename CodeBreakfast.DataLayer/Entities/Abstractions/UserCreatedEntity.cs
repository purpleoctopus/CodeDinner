using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBreakfast.DataLayer.Entities.Abstractions;

public abstract class UserCreatedEntity
{
    [ForeignKey("Author")]
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}