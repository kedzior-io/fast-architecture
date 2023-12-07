using FastArchitecture.Api;
using Oakton;
using Serilog;
using Wolverine;

/*
 * TODO: exmaple of Authorization
 * TODO: example of IActionFilter
 */

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.Console();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCaching();
builder.Services.AddSwaggerGen();
builder.Host.UseWolverine();
builder.Services.AddAuthorization();

//builder.Services.AddApiDependencies();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCaching();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    /*
     * Do not run this in production, not really recommended
     * https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#apply-migrations-at-runtime
     */

    //using var scope = app.Services.CreateScope();
    //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //dbContext.Database.Migrate();
}

app.AddEndpoints();
app.Run();
//return await app.RunOaktonCommands(args);