using FastArchitecture.Domain;

namespace FastArchitecture.Handlers.Orders;

public class GetOrderByNameTests
{
    private static readonly string OrderName = "#0001";

    private static GetOrderByName.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new GetOrderByName.Handler(HandlerContextFactory.GetHandlerContext(dbContext));
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var orders = new List<Order> {
            Order.Create(OrderName),
            Order.Create("#0002"),
        };

        dc.Orders.AddRange(orders);
        dc.SaveChanges();
    }

    [Fact]
    public async Task Get_Order_ReturnSingleOrder()
    {
        var dc = InMemoryDbContextFactory.Create();
        SetTestData(dc);

        var query = new GetOrderByName.Query()
        {
            Name = OrderName
        };

        var expected = OrderName;

        var response = await GetHandler(dc).ExecuteAsync(query, default);

        Assert.Equal(expected, response.Payload.Name);
    }
}