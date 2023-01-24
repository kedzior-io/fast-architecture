using FastEndpoints;
using FastArchitecture.Infrastructure.Persistence;

namespace FastArchitecture.Handlers.Abstractions;

public abstract class QueryHandler<TQuery, TResponse> : ICommandHandler<TQuery, TResponse>
         where TQuery : IQuery<TResponse>
{
    protected readonly IDbContext DbContext;

    protected QueryHandler(IHandlerContext context)
    {
        DbContext = context.DbContext;
    }

    public abstract Task<TResponse> ExecuteAsync(TQuery query, CancellationToken ct = default);
}