using FastEndpoints;
using FastArchitecture.Handlers.Orders.Queries;
using FastArchitecture.Core.Api;

namespace FastArchitecture.Api.Endpoints.Orders;

public class GetCountryListEndpoint : ApiEndpoint<GetCountries.Query, GetCountries.Response>
{
    public override void Configure()
    {
        Get("country.list");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(GetCountries.Query request, CancellationToken ct)
        => await SendAsync(await request.ExecuteAsync(ct));
}