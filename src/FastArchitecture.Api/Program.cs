using FastArchitecture.Handlers.Abstractions;
using FastEndpoints.Swagger;
using FastArchitecture.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddResponseCaching();
builder.Services.AddSwaggerDoc();
builder.Services.AddTransient<IHandlerContext, HandlerContext>();

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration
        .WriteTo.Console();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseCosmos("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "FastArchitecture", options =>
    {
        options.ConnectionMode(ConnectionMode.Direct);
    });
});

builder.Services.AddTransient<IDbContext, ApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCaching();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();