using FastArchitecture.Handlers.Orders.Queries;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetCountryListEndpoint : ApiEndpoint<GetCountries.Query, GetCountries.Response>
{
    public override void Configure()
    {
        Get("country.list");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetCountries.Query query, CancellationToken ct) => await SendAsync(query, ct);
}