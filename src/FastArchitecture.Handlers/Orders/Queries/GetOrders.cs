using FastArchitecture.Handlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetOrders
{
    public sealed class Query : IQuery<Response>
    {
    }

    public sealed class Response
    {
        public IReadOnlyCollection<OrderModel> Orders { get; private set; }

        public Response(IReadOnlyCollection<Domain.Order> orders)
        {
            Orders = orders.Select(x => new OrderModel(x)).ToList();
        }
    }

    public sealed class OrderModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public OrderModel(Domain.Order order)
        {
            Id = order.Id;
            Name = order.Name;
        }
    }

    public sealed class Handler : QueryHandler<Query, Response>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<Response> ExecuteAsync(Query query, CancellationToken ct)
        {
            var orders = await DbContext
                .Orders
                .ToListAsync(ct);

            return new Response(orders);
        }
    }
}