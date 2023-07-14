using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace FastArchitecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services/*, IConfiguration configuration, IHostEnvironment environment*/)
    {
        services.AddFastEndpoints(dicoveryOptions =>
        {
            dicoveryOptions.Assemblies = new[] { typeof(Handlers.Abstractions.CommandHandler<>).Assembly };
        });

        services.AddScoped<IHandlerRequestContext, HandlerRequestContext>();
        services.AddTransient<IHandlerContext, HandlerContext>();

        services.AddDbContext<ApplicationDbContext>();

        services.AddTransient<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}