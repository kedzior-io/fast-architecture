using Ardalis.GuardClauses;
using FastArchitecture.Domain.Abstractions;

namespace FastArchitecture.Domain;

public class Order : Entity, IAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string Status { get; private set; } = null!;
    public string UserId { get; private set; } = null!;

    private Order()
    {
        // EF
    }

    public Order(string name, string status, string userId)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.NullOrWhiteSpace(status);
        Guard.Against.NullOrWhiteSpace(userId);

        Name = name;
        Status = status;
        UserId = userId;
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
        return new Order(name, "draft", "1");
    }

    public static Order Create(string name)
    {
        return new Order(name, "created", "1");
    }
}