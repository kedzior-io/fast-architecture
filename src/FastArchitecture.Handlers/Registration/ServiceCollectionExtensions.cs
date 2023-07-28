using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

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

        return services;
    }
}