using FastArchitecture.Domain;

namespace FastArchitecture.Handlers.Orders;

public class GetOrderByNameTests
{
    private static readonly string OrderName1UserId = "chucknorris";

    private static readonly string OrderName1 = "#0001";
    private static readonly string OrderName2 = "#0002";

    private static GetOrderByName.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new GetOrderByName.Handler(HandlerContextFactory.GetHandlerContext(dbContext, userId: OrderName1UserId));
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var orders = new List<Order> {
            Order.Create(OrderName1, OrderName1UserId),
            Order.Create(OrderName2, OrderName1UserId),
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
            Name = OrderName1
        };

        var expected = OrderName1;

        var response = await GetHandler(dc).ExecuteAsync(query, default);

        Assert.Equal(expected, response.Payload.Name);
    }
}