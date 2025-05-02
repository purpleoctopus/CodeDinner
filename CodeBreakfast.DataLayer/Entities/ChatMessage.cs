using CodeBreakfast.DataLayer.Entities.Abstractions;

namespace CodeBreakfast.DataLayer.Entities;

public class ChatMessage : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Message { get; set; }
}