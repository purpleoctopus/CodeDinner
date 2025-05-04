using CodeBreakfast.Data.Entities.Abstractions;

namespace CodeBreakfast.Data.Entities;

public class Module : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}