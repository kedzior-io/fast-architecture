using FastArchitecture.Handlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetOrderByName
{
    public sealed class Query : IQuery<Response>
    {
        public string Name { get; set; } = "";
    }

    public sealed class Response
    {
        public OrderModel Order { get; private set; }

        public Response(Domain.Order order)
        {
            Order = new OrderModel(order);
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
            var order = await DbContext
                   .Orders
                   .Where(x => x.Name == query.Name)
                   .SingleOrDefaultAsync(ct);

            return new Response(order);
        }
    }
}