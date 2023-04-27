using FastArchitecture.Infrastructure.Persistence;

namespace FastArchitecture.Handlers.Abstractions;

/*
 * TODO 1: Inject HostEnvironment
 */

public sealed class HandlerContext : IHandlerContext
{
    public IDbContext DbContext { get; private set; }

    public IHandlerRequestContext RequestContext { get; private set; }

    public Serilog.ILogger Logger { get; private set; }

    public HandlerContext(IDbContext dbContext, IHandlerRequestContext requestContext, Serilog.ILogger logger/*, IHostEnvironment hostingEnvironment*/)
    {
        DbContext = dbContext;
        RequestContext = requestContext;
        Logger = logger;
    }
}