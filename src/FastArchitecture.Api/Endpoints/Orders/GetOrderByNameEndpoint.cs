using FastArchitecture.Handlers.Orders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrderByNameEndpoint : ApiEndpoint<GetOrderByName.Query, GetOrderByName.Response>
{
    public override void Configure()
    {
        Get("orders.getByName");
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] GetOrderByName.Query query, CancellationToken ct) => await SendAsync(query, ct);
}