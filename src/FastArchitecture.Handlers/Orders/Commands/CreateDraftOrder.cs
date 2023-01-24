using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;
using FluentValidation;
using Serilog;

namespace FastArchitecture.Handlers.Orders.Commands;

public static class CreateDraftOrder
{
    public sealed class Command : ICommand
    {
        public string Name { get; set; } = "";
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
        private readonly ILogger _logger;

        public Handler(IHandlerContext context) : base(context)
        {
            _logger = context.Logger;
        }

        public override async Task ExecuteAsync(Command command, CancellationToken ct)
        {
            var order = Domain.Order.CreateDraft(command.Name);

            await DbContext.Orders.AddAsync(order, ct);
            await DbContext.SaveChangesAsync(ct);

            _logger.Information("In need to log something here: {@order}", order);
        }
    }
}