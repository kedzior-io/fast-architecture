using FastArchitecture.Domain;
using FastArchitecture.Handlers.Commands;

namespace FastArchitecture.Handlers.Orders;

public class CreateOrderTests
{
    private static readonly string NewOrderName = "#0003";
    private static readonly string NewOrderStatus = "created";

    private static CreateOrder.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new CreateOrder.Handler(HandlerContextFactory.GetHandlerContext(dbContext));
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var orders = new List<Order> {
            Order.Create("#0001"),
            Order.Create("#0002"),
        };

        dc.Orders.AddRange(orders);
        dc.SaveChanges();
    }

    [Fact]
    public async Task Create_Order_ReturnEmpty()
    {
        var dc = InMemoryDbContextFactory.Create();
        SetTestData(dc);

        var command = new CreateOrder.Command()
        {
            Name = NewOrderName
        };

        var expectedName = NewOrderName;
        var expectedStatus = NewOrderStatus;

        await GetHandler(dc).ExecuteAsync(command, default);
        var result = dc.Orders.SingleOrDefault(o => o.Name == NewOrderName);

        Assert.NotNull(result);
        Assert.Equal(expectedName, result.Name);
        Assert.Equal(expectedStatus, result.Status);
    }

    [Fact]
    public void Create_Order_ReturnNameTooShort()
    {
        var dc = InMemoryDbContextFactory.Create();
        SetTestData(dc);

        var command = new CreateOrder.Command()
        {
            Name = ""
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await GetHandler(dc).ExecuteAsync(command, default));
    }
}