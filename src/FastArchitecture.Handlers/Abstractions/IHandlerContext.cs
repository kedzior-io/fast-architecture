using FastArchitecture.Infrastructure.Persistence;
using Serilog;

namespace FastArchitecture.Handlers.Abstractions;

public interface IHandlerContext
{
    IDbContext DbContext { get; }
    ILogger Logger { get; }
}