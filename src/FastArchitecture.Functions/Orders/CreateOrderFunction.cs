using FastArchitecture.Handlers.Commands;
using FastEndpoints;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace FastArchitecture.Functions.Orders;

/*
 * TODO 1: Trigger validation automatically
 * TODO 2: Inject ILogger and use FunctionBase to log all function requests and data
 */

public class CreateOrderFunction // : FunctionBase<CreateOrderFunction>
{
    private readonly IValidator<CreateOrder.Command> _validator;

    public CreateOrderFunction(IValidator<CreateOrder.Command> validator) //: base(logger)
    {
        _validator = validator;
    }

    [Function(nameof(CreateOrderFunction))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
    {
        var command = new CreateOrder.Command()
        {
            Name = "#1"
        };

        var validationResult = await _validator.ValidateAsync(command);

        await command.ExecuteAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
}