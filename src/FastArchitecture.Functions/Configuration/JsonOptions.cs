using System.Text.Json;
using System.Text.Json.Serialization;

namespace FastArchitecture.Functions.Configuration;

public static class JsonOptions
{
    public static readonly JsonSerializerOptions Defaults = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        WriteIndented = true,
    };
}