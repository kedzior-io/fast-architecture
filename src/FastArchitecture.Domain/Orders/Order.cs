using Ardalis.GuardClauses;
using FastArchitecture.Domain.Abstractions;

namespace FastArchitecture.Domain;

public class Order : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Status { get; private set; }

    public Order()
    {
        // EF
    }

    public Order(string name, string status)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(status, nameof(status));
        Name = name;
        Status = status;
    }

    public static Order CreateDraft(string name)
    {
        return new Order(name, "draft");
    }

    public static Order Create(string name)
    {
        return new Order(name, "created");
    }
}