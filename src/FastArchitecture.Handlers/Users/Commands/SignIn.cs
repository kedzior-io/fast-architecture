using FastEndpoints;
using FastArchitecture.Handlers.Abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FastArchitecture.Handlers.Commands;

public static class SignIn
{
    public sealed class Command : ICommand
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public sealed class MyValidator : Validator<Command>
    {
        public MyValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Invalid password");
        }
    }

    public sealed class Handler : CommandHandler<Command>
    {
        public Handler(IHandlerContext context) : base(context)
        {
        }

        public override async Task ExecuteAsync(Command command, CancellationToken ct)
        {
            if(command.Email == "" && command.Password)
            {
                return Ok();
            }
        }
    }
}