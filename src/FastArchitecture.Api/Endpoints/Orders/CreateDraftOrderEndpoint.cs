using FastEndpoints;
using FastArchitecture.Handlers.Orders.Commands;

using FastArchitecture.Core.Api;

namespace FastArchitecture.Api.Endpoints.Orders;

public class CreateDraftOrderEndpoint : ApiEndpoint<CreateDraftOrder.Command>
{
    public override void Configure()
    {
        Post("orders.create");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CreateDraftOrder.Command command, CancellationToken ct)
        => await SendAsync(await command.ExecuteAsync(ct));
}