﻿using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;
using FastArchitecture.Core.Api;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetOrdersV1Endpoint : ApiEndpoint<GetOrders.Query, GetOrders.Response>
{
    public override void Configure()
    {
        Get("orders.list"); // /v1/orders.list
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(GetOrders.Query request, CancellationToken ct)
        => await SendAsync(await request.ExecuteAsync(ct));
}