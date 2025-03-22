namespace CodeDinner.API.Models;

public class SessionModel
{
    public string Token { get; set; }
    public List<string> Roles { get; set; }
}