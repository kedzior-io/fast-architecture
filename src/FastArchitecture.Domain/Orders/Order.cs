using Ardalis.GuardClauses;
using FastArchitecture.Domain.Abstractions;

namespace FastArchitecture.Domain;

public class Order : Entity, IAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string Status { get; private set; } = null!;

    private Order()
    {
        // EF
    }

    public Order(string name, string status)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.NullOrWhiteSpace(status);

        Name = name;
        Status = status;
    }

    public void UpdateStatus(string status)
    {
        Guard.Against.NullOrWhiteSpace(status);
        Status = status;
    }

    public void SetConfrimed()
    {
        Status = "confrimed";
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