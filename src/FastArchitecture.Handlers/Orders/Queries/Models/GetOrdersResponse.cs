namespace FastArchitecture.Handlers.Orders.Queries.Models;

public sealed class GetOrdersResponse
{
    public IReadOnlyCollection<OrderListModel> Orders { get; private set; }

    public GetOrdersResponse(IReadOnlyCollection<Domain.Order> orders)
    {
        Orders = orders.Select(OrderListModel.Create).ToList();
    }
}