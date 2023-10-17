using FastArchitecture.Core.Constants;
using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace FastArchitecture.Handlers.Registration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiDependencies(this IServiceCollection services)
    {
        services.AddScoped<IHandlerContext, HandlerContext>();

        services.AddCommonDependencies();

        return services;
    }

    public static IServiceCollection AddAzureFunctionsDependencies(this IServiceCollection services)
    {
        services.AddScoped<IHandlerContext, FunctionHandlerContext>();
        services.AddDbContextFactory<ApplicationDbContext>();

        services.AddCommonDependencies();

        return services;
    }

    private static IServiceCollection AddCommonDependencies(this IServiceCollection services)
    {
        services.AddFastEndpoints(dicoveryOptions =>
        {
            dicoveryOptions.Assemblies = new[] { typeof(Abstractions.CommandHandler<>).Assembly };
        });

        services.AddScoped<IHandlerRequestContext, HandlerRequestContext>();
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddFusionCache()
            .WithSerializer(
                new FusionCacheSystemTextJsonSerializer()
            )
            .WithDistributedCache(
                new RedisCache(new RedisCacheOptions { Configuration = ConnectionStrings.Redis })
            )
            .WithBackplane(
                new RedisBackplane(new RedisBackplaneOptions { Configuration = ConnectionStrings.Redis })
            )
        ;

        return services;
    }
}