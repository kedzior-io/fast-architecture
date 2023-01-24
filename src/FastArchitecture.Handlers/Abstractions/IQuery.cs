using FastEndpoints;

namespace FastArchitecture.Handlers.Abstractions;

public interface IQuery<out TResponse> : ICommand<TResponse>
{
}