using System.ComponentModel.DataAnnotations;

namespace CodeBreakfast.Common.Models;

public class UserUpdateDto
{
    [Length(1,10)]
    public string FirstName { get; set; }
    public string? LastName { get; set; }
}

public class UserDetailDto : UserUpdateDto
{
    
}