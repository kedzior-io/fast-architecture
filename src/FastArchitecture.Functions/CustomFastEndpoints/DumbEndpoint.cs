using FastEndpoints;

namespace FastArchitecture.Functions.CustomFastEndpoints;

/// <summary>
/// That's a workaround gor having commands triggered from azure functions using FastEndpoints
/// </summary>
public record Dumb();

public class DumbEndpoint : Endpoint<Dumb>
{
    public override void Configure()
    {
        Post("dumb");
        AllowAnonymous();
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        await SendEmptyJsonObject(ct);
    }
}