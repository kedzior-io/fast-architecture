using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Handlers.Orders.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetOrders
{
    public sealed class Query : IQuery<IHandlerResponse<Response>>
    {
    }

    public sealed class Response
    {
        public IReadOnlyCollection<OrderListModel> Orders { get; private set; }

        public Response(IReadOnlyCollection<Domain.Order> orders)
        {
            Orders = orders.Select(OrderListModel.Create).ToList();
        }
    }

    public sealed class Handler : QueryHandler<Query, Response>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<IHandlerResponse<Response>> ExecuteAsync(Query query, CancellationToken ct)
        {
            var orders = await DbContext
                .Orders
                .ToListAsync(ct);

            return Success(new Response(orders));
        }
    }
}