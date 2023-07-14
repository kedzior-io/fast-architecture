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
            var cacheKey = "production:countries";

            _cache.DefaultEntryOptions.SkipBackplaneNotifications = true;

            var countries = await _cache.GetOrSetAsync(
                cacheKey,
                async _ => GetCountriesFromDb(),
                options => options.SetDuration(TimeSpan.FromMinutes(10)).SetSkipBackplaneNotifications(false),
                ct) ?? new List<string>();

            return Success(new Response() { Countries = countries });
        }
    }

    private static List<string> GetCountriesFromDb()
    {
        return new List<string> { "Spain", "United Stated of America" };
    }
}