namespace CodeBreakfast.Data.Entities;

public class Newsletter
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
}