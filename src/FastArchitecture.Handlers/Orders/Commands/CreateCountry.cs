using FastArchitecture.Core.Constants;
using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;
using ZiggyCreatures.Caching.Fusion;

namespace FastArchitecture.Handlers.Orders.Commands;

public static class CreateCountry
{
    public sealed class Command : ICommand
    {
        public string Name { get; set; } = "";
    }

    public sealed class MyValidator : Validator<Command>
    {
    }

    public sealed class Handler : Abstractions.CommandHandler<Command>
    {
        private readonly IFusionCache _cache;

        public Handler(IHandlerContext context, IFusionCache cache) : base(context)
        {
            _cache = cache;
        }

        public override async Task<IHandlerResponse> ExecuteAsync(Command command, CancellationToken ct)
        {
            _cache.DefaultEntryOptions.SkipBackplaneNotifications = false;

            var countries = await GetCountriesFromDb();

            countries.Add(command.Name);

            await _cache.SetAsync(CacheKeys.Countries, countries, options => options.SetDuration(TimeSpan.MaxValue).SetSkipBackplaneNotifications(false), token: ct);

            return Success();
        }

        private static async Task<List<string>> GetCountriesFromDb()
        {
            return await Task.FromResult(new List<string> { "Spain", "United Stated of America" });
        }
    }
}