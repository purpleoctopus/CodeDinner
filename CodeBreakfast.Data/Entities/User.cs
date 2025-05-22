using Microsoft.AspNetCore.Identity;

namespace CodeBreakfast.Data.Entities;

public class User : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<UserLesson> UserLessons { get; set; } = new List<UserLesson>();
    public DateTime RegisteredOn { get; set; }
    public string? ProfilePicture { get; set; }
}