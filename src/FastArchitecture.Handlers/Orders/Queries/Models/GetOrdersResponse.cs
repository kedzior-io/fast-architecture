namespace FastArchitecture.Handlers.Orders.Queries.Models;

public sealed class GetOrdersResponse
{
    public IReadOnlyCollection<OrderModel> Orders { get; private set; }

    public GetOrdersResponse(IReadOnlyCollection<Domain.Order> orders)
    {
        Orders = orders.Select(OrderModel.Create).ToList();
    }
}