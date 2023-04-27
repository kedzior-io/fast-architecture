using FastArchitecture.Domain;

namespace FastArchitecture.Handlers.Orders;

public class OrderTests
{
    private static readonly string NewOrderName = "#0003";
    private static readonly string NewOrderStatus = "created";
    private static readonly string NewOrderUserId = "1";

    [Fact]
    public void CreateOrder_EmptyName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Order(string.Empty, NewOrderStatus, NewOrderUserId));
    }

    [Fact]
    public void CreateOrder_EmptyStatus_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Order(NewOrderName, string.Empty, NewOrderUserId));
    }

    [Fact]
    public void CreateOrder_ValidParameters_ReturnsOrder()
    {
        var order = new Order(NewOrderName, NewOrderStatus, NewOrderUserId);

        Assert.NotNull(order);
        Assert.NotEmpty(order.Name);
        Assert.NotEmpty(order.Status);
    }
}