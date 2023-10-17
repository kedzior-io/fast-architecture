using FastArchitecture.Handlers.Orders.Queries;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrdersEndpoint : ApiEndpoint<GetOrders.Query, GetOrders.Response>
{
    public override void Configure()
    {
        Get("orders.list");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(GetOrders.Query query, CancellationToken ct) => await SendAsync(query, ct);
}