namespace FastArchitecture.UnitTests.Factories;

public static class HandlerContextFactory
{
    public static IHandlerContext GetHandlerContext(ApplicationDbContext dbContext)
    {
        return new HandlerContext(dbContext, LoggerFactory.Create());
    }
}