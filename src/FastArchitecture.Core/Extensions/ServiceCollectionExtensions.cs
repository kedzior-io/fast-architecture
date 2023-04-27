using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
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

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseCosmos("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "FastArchitecture");
        });

        services.AddTransient<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}