using FastEndpoints;
using FastArchitecture.Handlers.Abstractions;
using FluentValidation;

namespace FastArchitecture.Handlers.Commands;

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

    public sealed class Handler : Abstractions.CommandHandler<Command>
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