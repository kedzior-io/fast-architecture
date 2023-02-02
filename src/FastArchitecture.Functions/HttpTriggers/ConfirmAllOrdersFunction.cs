using FastArchitecture.Functions.Abstractions;
using FastArchitecture.Handlers.Commands;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace FastArchitecture.Functions.ServiceBusTriggers;

/// <summary>
/// An example of typical function triggered by a Azure Logic App
/// 
/// FunctionBase - takse care of logging, validation and triggering the command
/// 
/// </summary>
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