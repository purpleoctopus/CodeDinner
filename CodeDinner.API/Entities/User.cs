using Microsoft.AspNetCore.Identity;

namespace CodeDinner.API.Entities;

public class User : IdentityUser
{
    public string Username { get; set; }
}