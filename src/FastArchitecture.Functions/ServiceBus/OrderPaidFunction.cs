using FastArchitecture.Functions.Abstractions;
using FastArchitecture.Handlers.Commands;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Serilog;

namespace FastArchitecture.Functions.ServiceBus;

public class OrderPaidFunction : FunctionBase<OrderPaidFunction>
{
    public OrderPaidFunction(ILogger logger, IValidator<CreateOrder.Command> validator) : base(logger, validator)
    {
    }

    [Function(nameof(OrderPaidFunction))]
    public async Task Run([ServiceBusTrigger("test-queue", Connection = "ConnectionStrings:ServiceBus")] string json, FunctionContext context)
    {
        await ExecuteAsync<CreateOrder.Command>(json, context);
    }
}