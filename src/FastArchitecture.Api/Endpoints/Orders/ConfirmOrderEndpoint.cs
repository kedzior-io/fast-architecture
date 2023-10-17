using FastArchitecture.Handlers.Orders.Commands;

namespace FastArchitecture.Api.Endpoints.Orders;

public class ConfirmOrderEndpoint : ApiEndpoint<ConfirmOrder.Command>
{
    public override void Configure()
    {
        Post("orders.confirm");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ConfirmOrder.Command command, CancellationToken ct) => await SendAsync(command, ct);
}