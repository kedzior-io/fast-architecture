using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;

namespace FastArchitecture.Core.Api;

public class ApiEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected async Task SendAsync(ICommand command, CancellationToken cancellationToken)
    {
        await command.ExecuteAsync(cancellationToken);
        await SendNoContentAsync(cancellationToken);
    }

    protected async Task SendAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        await query.ExecuteAsync(cancellationToken);
        await SendNoContentAsync(cancellationToken);
    }
}

public class ApiEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected async Task SendAsync(ICommand<IHandlerResponse<TResponse>> command, CancellationToken cancellationToken)
    {
        var handlerResponse = await command.ExecuteAsync(cancellationToken);
        await SendAsync(handlerResponse.Payload, cancellation: cancellationToken);
    }

    protected async Task SendAsync(IQuery<IHandlerResponse<TResponse>> command, CancellationToken cancellationToken)
    {
        var handlerResponse = await command.ExecuteAsync(cancellationToken);
        await SendAsync(handlerResponse.Payload, cancellation: cancellationToken);
    }
}