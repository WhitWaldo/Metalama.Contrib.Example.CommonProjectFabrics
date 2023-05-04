using Shared.Interfaces;

namespace Models;

public record Person(string Name) : IHasId
{
    public Guid Id { get; init; } = Guid.NewGuid();
}