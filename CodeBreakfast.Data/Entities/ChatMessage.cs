using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class ChatMessage : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
}