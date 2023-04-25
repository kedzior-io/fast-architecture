using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;
using Microsoft.AspNetCore.Mvc;
using FastArchitecture.Core.Api;
using FastArchitecture.Handlers.Orders.Queries.Models;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrderByNameEndpoint : ApiEndpoint<GetOrderByName.Query, GetOrderByName.Response>
{
    public override void Configure()
    {
        Get("orders.getByName");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] GetOrderByName.Query request, CancellationToken ct)
        => await SendAsync(await request.ExecuteAsync(ct));
}