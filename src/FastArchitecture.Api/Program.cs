using FastArchitecture.Handlers.Registration;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Serilog;

/*
 * TODO: exmaple of Authorization
 * TODO: example of IActionFilter
 */

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration
        .WriteTo.Console();
});

builder.Services.AddResponseCaching();
builder.Services.AddSwaggerDoc(s =>
    {
        s.DocumentName = "Initial Release";
        s.Title = "FastArchitecture.Api";
        s.Version = "v1.0";
    })
    .AddSwaggerDoc(maxEndpointVersion: 1, settings: s =>
    {
        s.DocumentName = "Release 1.0";
        s.Title = "FastArchitecture.Api";
        s.Version = "v1.0";
    });

builder.Services.AddDependencies();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCaching();
app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});

app.UseSwaggerGen();

if (app.Environment.IsDevelopment())
{
    /*
     * Do not run this in production, not reall recommended
     * https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#apply-migrations-at-runtime
     */

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}

app.Run();