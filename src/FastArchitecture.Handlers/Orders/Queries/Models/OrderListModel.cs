namespace FastArchitecture.Handlers.Orders.Queries.Models;

public sealed class OrderListModel
{
    public string Name { get; private set; }

    public OrderListModel(Domain.Order order)
    {
        Name = order.Name;
    }

    public static OrderListModel Create(Domain.Order order)
    {
        return new OrderListModel(order);
    }
}