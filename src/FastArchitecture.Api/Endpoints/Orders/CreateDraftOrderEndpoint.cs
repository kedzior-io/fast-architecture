using FastEndpoints;
using FastArchitecture.Handlers.Orders.Commands;

namespace FastArchitecture.Api.Endpoints.Orders;

public class CreateDraftOrderEndpoint : Endpoint<CreateDraftOrder.Command>
{
    public override void Configure()
    {
        Post("orders.create");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CreateDraftOrder.Command command, CancellationToken ct)
    {
        await command.ExecuteAsync(ct: ct);
        await SendNoContentAsync(cancellation: ct);
    }
}