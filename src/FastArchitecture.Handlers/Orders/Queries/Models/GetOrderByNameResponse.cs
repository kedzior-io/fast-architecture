namespace FastArchitecture.Handlers.Orders.Queries.Models;

public sealed class GetOrderByNameResponse
{
    public OrderModel Order { get; private set; }

    public GetOrderByNameResponse(Domain.Order order)
    {
        Order = new OrderModel(order);
    }
}