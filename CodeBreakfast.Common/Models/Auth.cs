namespace CodeBreakfast.Common.Models;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterDto : LoginDto
{
    
}