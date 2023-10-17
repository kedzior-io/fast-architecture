using FastArchitecture.Handlers.Orders.Commands;

namespace FastArchitecture.Api.Endpoints.Orders;

public class CreateCountryEndpoint : ApiEndpoint<CreateCountry.Command>
{
    public override void Configure()
    {
        Post("country.create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCountry.Command command, CancellationToken ct) => await SendAsync(command, ct);
}