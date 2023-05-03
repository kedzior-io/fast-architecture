using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;

namespace FastArchitecture.Handlers.Abstractions;

public abstract class QueryHandler<TQuery, TResponse> : ICommandHandler<TQuery, IHandlerResponse<TResponse>>
         where TQuery : IQuery<IHandlerResponse<TResponse>>
{
    protected readonly IDbContext DbContext;
    protected readonly IHandlerRequestContext RequestContext;

    protected QueryHandler(IHandlerContext context)
    {
        DbContext = context.DbContext;
        RequestContext = context.RequestContext;
    }

    public abstract Task<IHandlerResponse<TResponse>> ExecuteAsync(TQuery query, CancellationToken ct = default);

    public IHandlerResponse<TResponse> Success(TResponse response)
    {
        return HandlerResponse<TResponse>.Create(response);
    }

    public Task<IHandlerResponse<TResponse>> SuccessAsync(TResponse response)
    {
        return Task.FromResult(Success(response));
    }
}