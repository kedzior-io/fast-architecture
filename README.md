# Fast Architecture Solution Template .NET7

This is a solution template for creating ASP.NET Core Web API that uses [FastEndpoints](https://fast-endpoints.com) and its command bus to get close to CQRS pattern with DDD (Domain Driven Design) design approach.

The motive behind it is to create a solution that is

- easy to start with
- it is consistent
- has simple structure

## Project structure

- Api - thin API using [FastEndpoints](https://fast-endpoints.com) and [FastEndpoints Commands](https://fast-endpoints.com/docs/command-bus#_1-define-a-command)
- Functions - thin Azure Functions using [FastEndpoints Commands](https://fast-endpoints.com/docs/command-bus#_1-define-a-command)
- Handlers - class library where all command handlers live
- Domain - where all domain entities and service lives
- Infrastructure - persistence setup, sending emails etc
- UnitTests - project containing unit tests for domain entities and handlers
- IntegrationTests - still TODO

## Installation

Just fire up with Visual Studio or Rider.
For the simplicity the solution uses CosmosDB as persistence (using [emulator](https://aka.ms/cosmosdb-emulator) running on the localhost).

## Usage

1. Create an endpoint:

```csharp
public class GetOrdersEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("orders.list");
        AllowAnonymous();
        ResponseCache(60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var order = await new GetOrders.Query()
            .ExecuteAsync(ct: ct);

        await SendAsync(order, cancellation: ct);
    }
}
```

2. Create a `query`

```csharp
public static class GetOrders
{
    public sealed class Query : IQuery<Response>
    {
    }

    public sealed class Response
    {
        public IReadOnlyCollection<OrderModel> Orders { get; private set; }

        public Response(IReadOnlyCollection<Domain.Order> orders)
        {
            Orders = orders.Select(x => new OrderModel(x)).ToList();
        }
    }

    public sealed class OrderModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public OrderModel(Domain.Order order)
        {
            Id = order.Id;
            Name = order.Name;
        }
    }

    public sealed class Handler : QueryHandler<Query, Response>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<Response> ExecuteAsync(Query query, CancellationToken ct)
        {
            var orders = await DbContext
                .Orders
                .ToListAsync(ct);

            return new Response(orders);
        }
    }
}
```

and that's it!

Want to create a `command` with validation?

```csharp
public static class CreateOrder
{
    public sealed class Command : ICommand
    {
        public string Name { get; set; } = "";
    }

    public sealed class MyValidator : Validator<Command>
    {
        public MyValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("Order name is too short!");
        }
    }

    public sealed class Handler : CommandHandler<Command>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task ExecuteAsync(Command command, CancellationToken ct)
        {
            var order = Domain.Order.Create(command.Name);

            await DbContext.Orders.AddAsync(order, ct);
            await DbContext.SaveChangesAsync(ct);
        }
    }
}
```

Want to fire up a command from `Azure Function`? 

```csharp
public class ConfirmAllOrdersFunction : FunctionBase<ConfirmAllOrdersFunction>
{

    public ConfirmAllOrdersFunction(ILogger logger) : base(logger)
    {
    }

    [Function(nameof(ConfirmAllOrdersFunction))]
    public async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        var command = new ConfirmAllOrders.Command();

        await ExecuteAsync<ConfirmAllOrders.Command>(command, req.FunctionContext);
    }
}
```
Need to validate automatically and deserialize (useful when using [ServiceBusTrigger](https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger?tabs=isolated-process%2Cextensionv5&pivots=programming-language-csharp#example))?  

```csharp
public class OrderPaidFunction : FunctionBase<OrderPaidFunction>
{

    public OrderPaidFunction(ILogger logger, IValidator<CreateOrder.Command> validator) : base(logger, validator)
    {
    }
    
    [Function(nameof(OrderPaidFunction))]
    public async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        var json = "{\r\n  \"name\": \"AAA\"\r\n}";

        await ExecuteAsync<CreateOrder.Command>(json, req.FunctionContext);
    }
}
```


## TODO

There are few things to work out here and mainly:

- ~~fire up validation on Azure Function execution~~
- avoid creating `webAppBuilder` in Azure Function startup
- removing `DumbEndpoint`- just see the comments in the code
- add an example of integration test
- add an example of authorization endpoint
- add an example of usage of [Filters in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-7.0)

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
