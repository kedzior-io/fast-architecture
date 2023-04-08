using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrdersV1Endpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("orders.list"); // /v1/orders.list
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var order = await new GetOrders.Query().ExecuteAsync(ct: ct);
        await SendAsync(order, cancellation: ct);
    }
}