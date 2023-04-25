using FastArchitecture.Domain;

namespace FastArchitecture.Handlers.Orders;

public class GetOrdersTests
{
    private static GetOrders.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new GetOrders.Handler(HandlerContextFactory.GetHandlerContext(dbContext));
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var orders = new List<Order> {
             Order.Create("#0001"),
             Order.Create("#0002")
        };

        dc.Orders.AddRange(orders);
        dc.SaveChanges();
    }

    [Fact]
    public async Task Get_Orders_ReturnList()
    {
        var dbc = InMemoryDbContextFactory.Create();
        SetTestData(dbc);

        var query = new GetOrders.Query();
        var expected = 2;

        var response = await GetHandler(dbc).ExecuteAsync(query, default);

        Assert.Equal(expected, response.Payload.Orders.Count);
    }
}