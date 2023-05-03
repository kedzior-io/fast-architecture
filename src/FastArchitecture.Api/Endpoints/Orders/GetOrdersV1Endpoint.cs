using FastArchitecture.Core.Api;
using FastArchitecture.Handlers.Orders.Queries;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrdersV1Endpoint : ApiEndpoint<GetOrders.Query, GetOrders.Response>
{
    public override void Configure()
    {
        Get("orders.list"); // /v1/orders.list
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(GetOrders.Query query, CancellationToken ct) => await SendAsync(query, ct);
}