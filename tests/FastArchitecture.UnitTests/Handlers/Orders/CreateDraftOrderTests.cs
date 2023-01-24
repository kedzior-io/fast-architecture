using FastArchitecture.Domain;

namespace FastArchitecture.Handlers.Orders;

public class CreateDraftOrderTests
{
    private static readonly string NewOrderName = "#0003";
    private static readonly string NewOrderStatus = "draft";

    private static CreateDraftOrder.Handler GetHandler(ApplicationDbContext dbContext)
    {
        return new CreateDraftOrder.Handler(HandlerContextFactory.GetHandlerContext(dbContext));
    }

    private static void SetTestData(ApplicationDbContext dc)
    {
        var orders = new List<Order> {
            Order.CreateDraft("#0001"),
            Order.CreateDraft("#0002"),
        };

        dc.Orders.AddRange(orders);
        dc.SaveChanges();
    }

    [Fact]
    public async Task Create_DraftOrder_ReturnEmpty()
    {
        var dc = InMemoryDbContextFactory.Create();
        SetTestData(dc);

        var command = new CreateDraftOrder.Command()
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
}