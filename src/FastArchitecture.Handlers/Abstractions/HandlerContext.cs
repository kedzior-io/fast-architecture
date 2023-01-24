using FastArchitecture.Infrastructure.Persistence;

namespace FastArchitecture.Handlers.Abstractions;

/*
 * TODO 1: Inject HostEnvironment
 * TODO 2: Inject RequestContext (object that handle getting data from headers/claims etc: language, country code, userId etc)
 */

public sealed class HandlerContext : IHandlerContext
{
    public IDbContext DbContext { get; private set; }

    //public IHandlerRequestContext RequestContext { get; private set; }

    public Serilog.ILogger Logger { get; private set; }

    //public IHostEnvironment HostEnvironment { get; private set; }

    public HandlerContext(IDbContext dbContext, /*IHandlerRequestContext requestContext,*/ Serilog.ILogger logger/*, IHostEnvironment hostingEnvironment*/)
    {
        DbContext = dbContext;
        //RequestContext = requestContext;
        Logger = logger;
        //HostEnvironment = hostingEnvironment;
    }
}