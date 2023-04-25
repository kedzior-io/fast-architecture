using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;
using FluentValidation;
using Serilog;

namespace FastArchitecture.Handlers.Orders.Commands;

public static class CreateDraftOrder
{
    public sealed class Command : ICommand<IHandlerResponse<Response>>
    {
        public string Name { get; set; } = "";
    }

    // Handler response can sit within handler (here) or in "Models" and be shared, that's up to you!
    public sealed class Response
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CustomerName { get; private set; }

        public Response(Domain.Order order)
        {
            Id = order.Id;
            Name = order.Name;
            CustomerName = order.Name;
        }

        public static Response Create(Domain.Order order)
        {
            return new Response(order);
        }
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

    public sealed class Handler : Abstractions.CommandHandler<Command, Response>
    {
        private readonly ILogger _logger;

        public Handler(IHandlerContext context) : base(context)
        {
            _logger = context.Logger;
        }

        public override async Task<IHandlerResponse<Response>> ExecuteAsync(Command command, CancellationToken ct)
        {
            var order = Domain.Order.CreateDraft(command.Name);

            await DbContext.Orders.AddAsync(order, ct);
            await DbContext.SaveChangesAsync(ct);

            _logger.Information("In need to log something here: {@order}", order);

            return Success(new Response(order));
        }
    }
}