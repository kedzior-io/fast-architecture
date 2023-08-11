
using FastArchitecture.Handlers.Abstractions;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace FastArchitecture.Handlers.Orders.Commands;

public static class ConfirmAllOrders
{
    public sealed class Command : ICommand
    {
    }
    
    public sealed class Handler : Abstractions.CommandHandler<Command>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task<IHandlerResponse> ExecuteAsync(Command command, CancellationToken ct = default)
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