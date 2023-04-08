using FastArchitecture.Handlers.Abstractions;
using FastArchitecture.Handlers.Orders.Queries.Models;
using FastEndpoints;
using FluentValidation;
using Serilog;

namespace FastArchitecture.Handlers.Orders.Commands;

public static class CreateDraftOrder
{
    public sealed class Command : ICommand<OrderModel>
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
        private readonly ILogger _logger;

        public Handler(IHandlerContext context) : base(context)
        {
            _logger = context.Logger;
        }

        public override async Task<OrderModel> ExecuteAsync(Command command, CancellationToken ct)
        {
            var order = Domain.Order.CreateDraft(command.Name);

            await DbContext.Orders.AddAsync(order, ct);
            await DbContext.SaveChangesAsync(ct);

            _logger.Information("In need to log something here: {@order}", order);

            return new OrderModel(order);
        }
    }
}