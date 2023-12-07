namespace FastArchitecture.Handlers.Orders.Queries;

public static class ListOrders
{
    public class Query
    {
    }

    public record Response(IReadOnlyCollection<OrderModel> Orders);

    public record OrderModel(string Id);

    public class Handler
    {
        public Handler()
        {
        }

        public Response Handle(Query query)
        {
            return new Response(new List<OrderModel>());
        }
    }
}