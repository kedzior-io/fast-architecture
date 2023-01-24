using FastEndpoints;
using FastArchitecture.Infrastructure.Persistence;

namespace FastArchitecture.Handlers.Abstractions;

public abstract class CommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
         where TCommand : ICommand<TResponse>
{
    protected readonly IDbContext DbContext;

    protected CommandHandler(IHandlerContext context)
    {
        DbContext = context.DbContext;
    }

    public abstract Task<TResponse> ExecuteAsync(TCommand command, CancellationToken ct = default);
}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
         where TCommand : ICommand
{
    protected readonly IDbContext DbContext;

    protected CommandHandler(IHandlerContext context)
    {
        DbContext = context.DbContext;
    }

    public abstract Task ExecuteAsync(TCommand command, CancellationToken ct = default);
}