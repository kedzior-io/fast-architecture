using FastArchitecture.Functions.Abstractions;
using FastArchitecture.Handlers.Commands;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace FastArchitecture.Functions.ServiceBusTriggers;

public class OrderPaidFunction : FunctionBase<OrderPaidFunction>
{

    public OrderPaidFunction(ILogger logger, IValidator<CreateOrder.Command> validator) : base(logger, validator)
    {
    }

    /// <summary>
    ///  An example of typical function triggered by Azure Service Bus queue with the follwoing trigger:
    ///  
    ///  public async Task Run([ServiceBusTrigger("order-paid-queue", Connection = "Connections:ServiceBus")] string json, FunctionContext context)
    ///  
    /// </summary>


    [Function(nameof(OrderPaidFunction))]
    public async Task Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        var json = "{\r\n  \"name\": \"AAA\"\r\n}";

        await ExecuteAsync<CreateOrder.Command>(json, req.FunctionContext);
    }
}