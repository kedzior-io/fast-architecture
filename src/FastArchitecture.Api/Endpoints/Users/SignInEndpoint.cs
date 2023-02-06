using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;
using FastArchitecture.Handlers.Orders.Commands;

namespace FastArchitecture.Api.Endpoints.Orders;

public class SignInEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("users.signIn");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SignIn.Command command, CancellationToken ct)
    {
        await SendAsync(command, cancellation: ct);
    }
}