using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrderByNameEndpoint : Endpoint<GetOrderByName.Query>
{
    public override void Configure()
    {
        Get("orders.getByName");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] GetOrderByName.Query request, CancellationToken ct)
    {
        var order = await request.ExecuteAsync(ct: ct);
        await SendAsync(order, cancellation: ct);
    }
}