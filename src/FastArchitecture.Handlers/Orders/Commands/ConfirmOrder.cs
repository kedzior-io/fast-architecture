using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Commands;

public static class ConfirmAllOrders
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

        public override async Task<IHandlerResponse> ExecuteAsync(Command command, CancellationToken ct)
        {
            var orders = await DbContext
                   .Orders
                   .ToListAsync(ct);

            orders.ForEach(x => x.SetConfrimed());

            await DbContext.SaveChangesAsync(ct);

            return Success();
        }
    }
}