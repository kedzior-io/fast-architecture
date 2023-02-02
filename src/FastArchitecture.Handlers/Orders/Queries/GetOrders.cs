using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Handlers.Orders.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetOrders
{
    public sealed class Query : IQuery<GetOrdersResponse>
    {
    }

    public sealed class Handler : QueryHandler<Query, GetOrdersResponse>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<GetOrdersResponse> ExecuteAsync(Query query, CancellationToken ct)
        {
            var orders = await DbContext
                .Orders
                .ToListAsync(ct);

            return new GetOrdersResponse(orders);
        }
    }
}