using FastArchitecture.Core.Constants;
using FastArchitecture.Handlers.Abstractions;
using ZiggyCreatures.Caching.Fusion;

namespace FastArchitecture.Handlers.Orders.Queries;

public static class GetCountries
{
    public sealed class Query : IQuery<IHandlerResponse<Response>>
    {
    }

    public sealed class Response
    {
        public IReadOnlyCollection<string> Countries { get; set; } = Array.Empty<string>();
    }

    public sealed class Handler : QueryHandler<Query, Response>
    {
        private readonly IFusionCache _cache;

        public Handler(IHandlerContext context, IFusionCache cache) : base(context)
        {
            _cache = cache;
        }

        public override async Task<IHandlerResponse<Response>> ExecuteAsync(Query query, CancellationToken ct)
        {
            _cache.DefaultEntryOptions.SkipBackplaneNotifications = true;

            var countries = await _cache.GetOrSetAsync(
                CacheKeys.Countries,
                factory: async _ => await GetCountriesFromDb(),
                options => options.SetDuration(TimeSpan.Zero).SetSkipBackplaneNotifications(false),
                ct) ?? new List<string>();

            return Success(new Response() { Countries = countries });
        }

        private static async Task<List<string>> GetCountriesFromDb()
        {
            return await Task.FromResult(new List<string> { "Spain", "United Stated of America" });
        }
    }
}