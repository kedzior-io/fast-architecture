using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FastArchitecture.Handlers.Abstractions;

public interface IHandlerRequestContext
{
    string? UserId { get; }
    string? Email { get; }
    string? LanguageCode { get; }
}

public sealed class HandlerRequestContext : IHandlerRequestContext
{
    public string? UserId { get; private set; }
    public string? Email { get; private set; }
    public string? LanguageCode { get; private set; }

    public HandlerRequestContext(string? userId, string? email, string? languageCode)
    {
        UserId = userId;
        Email = email;
        LanguageCode = languageCode;
    }

    public HandlerRequestContext(IHttpContextAccessor accessor)
    {
        if (accessor.HttpContext is not null)
        {
            UserId = accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Email = accessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        // TODO: add RequestCulture
        // LanguageCode = accessor.HttpContext.Features.Get<IRequestCultureFeature>()!.RequestCulture.Culture.TwoLetterISOLanguageName;
    }
}