using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Handlers.Registration;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

/*
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

        var logger = loggerConfiguration.CreateLogger();

        s.AddSingleton<ILogger>(_ => logger);
        s.AddValidatorsFromAssemblyContaining<HandlerContext>();

        var webAppBuilder = WebApplication.CreateBuilder();

        webAppBuilder.Services.AddSingleton<ILogger>(_ => logger);
        webAppBuilder.Services.AddAzureFunctionsDependencies();

        var app = webAppBuilder.Build();

        app.UseFastEndpoints();
    })
    .Build();

host.Run();