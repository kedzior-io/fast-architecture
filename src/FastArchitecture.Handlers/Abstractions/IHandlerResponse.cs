namespace FastArchitecture.Handlers.Abstractions;

public interface IHandlerResponse
{
}

public interface IHandlerResponse<out TResponse> : IHandlerResponse
{
    TResponse Payload { get; }
}

internal class HandlerResponse : IHandlerResponse
{
    internal static IHandlerResponse CreateEmpty()
    {
        return new HandlerResponse();
    }
}

internal sealed class HandlerResponse<TResponse> : HandlerResponse, IHandlerResponse<TResponse>
{
    internal HandlerResponse(TResponse payload)
    {
        Payload = payload;
    }

    public TResponse Payload { get; private set; }

    internal static IHandlerResponse<TResponse> Create(TResponse payload)
    {
        return new HandlerResponse<TResponse>(payload);
    }
}