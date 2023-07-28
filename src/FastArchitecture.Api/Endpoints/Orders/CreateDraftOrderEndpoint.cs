using FastArchitecture.Handlers.Orders.Commands;

namespace FastArchitecture.Api.Endpoints.Orders;

public class CreateDraftOrderEndpoint : ApiEndpoint<CreateDraftOrder.Command, CreateDraftOrder.Response>
{
    public override void Configure()
    {
        Post("orders.create");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CreateDraftOrder.Command command, CancellationToken ct) => await SendAsync(command, ct);
}