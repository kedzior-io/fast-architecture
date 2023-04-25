using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;

namespace FastArchitecture.Core.Api;

public class ApiEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull
{
    protected Task SendAsync(IHandlerResponse handlerResponse)
    {
        return SendNoContentAsync();
    }
}

public class ApiEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse> where TRequest : notnull
{
    protected Task SendAsync(IHandlerResponse<TResponse> handlerResponse)
    {
        return SendAsync(handlerResponse.Payload);
    }
}