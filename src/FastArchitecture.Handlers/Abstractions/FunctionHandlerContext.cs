using FastArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Abstractions;

public sealed class FunctionHandlerContext : IHandlerContext
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextfactory;

    public IDbContext DbContext => _dbContextfactory.CreateDbContext();

    public IHandlerRequestContext RequestContext { get; private set; }

    public Serilog.ILogger Logger { get; private set; }

    public FunctionHandlerContext(IDbContextFactory<ApplicationDbContext> dbContextfactory, IHandlerRequestContext requestContext, Serilog.ILogger logger/*, IHostEnvironment hostingEnvironment*/)
    {
        _dbContextfactory = dbContextfactory;
        RequestContext = requestContext;
        Logger = logger;
    }
}