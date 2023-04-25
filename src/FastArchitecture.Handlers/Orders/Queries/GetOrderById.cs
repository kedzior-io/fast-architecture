using FastArchitecture.Handlers.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetOrderByName
{
    public sealed class Query : IQuery<IHandlerResponse<Response>>
    {
        public string Name { get; set; } = "";
    }

    public sealed class Response
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CustomerName { get; private set; }

        public Response(Domain.Order order)
        {
            Id = order.Id;
            Name = order.Name;
            CustomerName = order.Name;
        }

        public static Response Create(Domain.Order order)
        {
            return new Response(order);
        }
    }

    public sealed class Handler : QueryHandler<Query, Response>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<IHandlerResponse<Response>> ExecuteAsync(Query query, CancellationToken ct)
        {
            var order = await DbContext
                   .Orders
                   .Where(x => x.Name == query.Name)
                   .SingleAsync(ct);

            return Success(new Response(order));
        }
    }
}