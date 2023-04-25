using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;
using FastArchitecture.Core.Api;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrdersEndpoint : ApiEndpoint<GetOrders.Query, GetOrders.Response>
{
    public override void Configure()
    {
        Get("orders.list"); // orders.list
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(GetOrders.Query request, CancellationToken ct)
        => await SendAsync(await request.ExecuteAsync(ct));
}