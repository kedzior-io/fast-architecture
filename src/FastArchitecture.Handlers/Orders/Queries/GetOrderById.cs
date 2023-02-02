using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Handlers.Orders.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetOrderByName
{
    public sealed class Query : IQuery<GetOrderByNameResponse>
    {
        public string Name { get; set; } = "";
    }

    public sealed class Handler : QueryHandler<Query, GetOrderByNameResponse>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<GetOrderByNameResponse> ExecuteAsync(Query query, CancellationToken ct)
        {
            var order = await DbContext
                   .Orders
                   .Where(x => x.Name == query.Name)
                   .SingleAsync(ct);

            return new GetOrderByNameResponse(order);
        }
    }
}