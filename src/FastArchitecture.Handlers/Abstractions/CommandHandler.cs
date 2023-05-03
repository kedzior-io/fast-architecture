using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;

namespace FastArchitecture.Handlers.Abstractions;

public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, IHandlerResponse<TResponse>> where TCommand : ICommand<IHandlerResponse<TResponse>>
{
    protected readonly IDbContext DbContext;

    protected CommandHandler(IHandlerContext context)
    {
        DbContext = context.DbContext;
    }

    public abstract Task<IHandlerResponse<TResponse>> ExecuteAsync(TCommand command, CancellationToken ct = default);

    public IHandlerResponse<TResponse> Success(TResponse response)
    {
        return HandlerResponse<TResponse>.Create(response);
    }
}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    protected readonly IDbContext DbContext;

    protected CommandHandler(IHandlerContext context)
    {
        DbContext = context.DbContext;
    }

    public abstract Task ExecuteAsync(TCommand command, CancellationToken ct = default);

    public IHandlerResponse Success()
    {
        return HandlerResponse.CreateEmpty();
    }

    public IHandlerResponse Error(string error)
    {
        return HandlerResponse.CreateEmpty();
    }
}