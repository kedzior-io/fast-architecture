using FastEndpoints.Swagger;
using FastEndpoints;
using Serilog;
using FastArchitecture.Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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

builder.Services.AddAuthentication()
                .AddJwtBearer(jcg =>
                {
                    jcg.RequireHttpsMetadata = false;
                    jcg.SaveToken = true;
                    jcg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("app-secret")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

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

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCaching();
app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.PrependToRoute = true;
});


app.UseSwaggerGen();
app.Run();