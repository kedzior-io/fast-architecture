using FastArchitecture.Handlers.Orders.Queries;
using Wolverine;

namespace FastArchitecture.Api;

public static class MyEndpoints
{
    public static IApplicationBuilder AddEndpoints(this WebApplication app)
    {
        app.MapGet("/orders.list", (ListOrders.Query query, IMessageBus bus) => bus.InvokeAsync(query));
        return app;
    }
}