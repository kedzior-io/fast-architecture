using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrdersEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("orders.list");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var order = await new GetOrders.Query()
            .ExecuteAsync(ct: ct);

        await SendAsync(order, cancellation: ct);
    }
}