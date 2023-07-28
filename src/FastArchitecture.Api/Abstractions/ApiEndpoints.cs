using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;

namespace FastArchitecture.Api;

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

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, cancellation: cancellationToken);
            return;
        }

        AddError(handlerResponse.Message);

        await SendErrorsAsync(cancellation: cancellationToken);
    }

    protected async Task SendAsync(IQuery<IHandlerResponse<TResponse>> command, CancellationToken cancellationToken)
    {
        var handlerResponse = await command.ExecuteAsync(cancellationToken);

        if (handlerResponse.IsSuccess)
        {
            await SendAsync(handlerResponse.Payload, cancellation: cancellationToken);
            return;
        }

        AddError(handlerResponse.Message);

        await SendErrorsAsync(cancellation: cancellationToken);
    }
}