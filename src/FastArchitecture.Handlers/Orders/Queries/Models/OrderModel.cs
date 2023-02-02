namespace FastArchitecture.Handlers.Orders.Queries.Models;

public sealed class OrderModel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public OrderModel(Domain.Order order)
    {
        Id = order.Id;
        Name = order.Name;
    }

    public static OrderModel Create(Domain.Order order)
    {
        return new OrderModel(order);
    }
}