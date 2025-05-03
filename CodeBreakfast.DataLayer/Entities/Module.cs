using CodeBreakfast.DataLayer.Entities.Abstractions;

namespace CodeBreakfast.DataLayer.Entities;

public class Module : UserCreatedEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}