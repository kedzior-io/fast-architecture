using FastArchitecture.Handlers.Orders.Commands;

namespace FastArchitecture.Api.Endpoints.Orders;


/*
 Using ApiEndpoint<ConfirmAllOrders.Command> works if a valid empty json body is sent aka {} or null otherwise the endpoint will return status code 415
 If empty body post is needed to be sent it requires to inherit from ApiEndpointWithoutRequest
 */
public class ConfirmAllOrdersEndpoint : ApiEndpointWithoutRequest<ConfirmAllOrders.Command>
{
    public override void Configure()
    {
        Post("orders.confirm.all");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) => await SendAsync(ct);
}