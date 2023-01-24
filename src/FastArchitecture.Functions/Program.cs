using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

/*
 * TODO 1: Share that code with FastArchitecture.Api and remove duplication here
 * TODO 2: Avoid creating webAppBuilder (required to use FastEndpoints)
 */

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.WithProperty("SourceLogger", "Serilog");

        // s.AddSingleton<ILogger>(_ => loggerConfiguration.CreateLogger());
        s.AddValidatorsFromAssemblyContaining<HandlerContext>();

        var webAppBuilder = WebApplication.CreateBuilder();

        webAppBuilder.Services.AddFastEndpoints(dicoveryOptions =>
        {
            dicoveryOptions.Assemblies = new[] { typeof(CommandHandler<>).Assembly };
        });

        webAppBuilder.Services.AddSingleton<ILogger>(_ => loggerConfiguration.CreateLogger());
        webAppBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseCosmos("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "FastArchitecture", options =>
            {
                options.ConnectionMode(ConnectionMode.Direct);
            });
        });

        webAppBuilder.Services.AddTransient<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        webAppBuilder.Services.AddTransient<IHandlerContext, HandlerContext>();

        var app = webAppBuilder.Build();
        app.UseFastEndpoints();
    })
    .Build();

host.Run();