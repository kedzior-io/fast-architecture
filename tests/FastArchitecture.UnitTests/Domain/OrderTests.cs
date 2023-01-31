using FastArchitecture.Domain;

namespace FastArchitecture.Handlers.Orders;

public class OrderTests
{
    private static readonly string NewOrderName = "#0003";
    private static readonly string NewOrderStatus = "created";

    [Fact]
    public void CreateOrder_EmptyName_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Order(string.Empty, NewOrderStatus));
    }

    [Fact]
    public void CreateOrder_EmptyStatus_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Order(NewOrderName, string.Empty));
    }

    [Fact]
    public void CreateOrder_ValidParameters_ReturnsOrder()
    {
        var order = new Order(NewOrderName, NewOrderStatus);

        Assert.NotNull(order);
        Assert.NotEmpty(order.Name);
        Assert.NotEmpty(order.Status);
    }
}