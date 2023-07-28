using FastArchitecture.Functions.Configuration;
using FastArchitecture.Functions.Logging;

using FastEndpoints;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Serilog;
using SerilogTimings.Extensions;
using System.Text.Json;

namespace FastArchitecture.Functions.Abstractions;

public abstract class FunctionBase<T> where T : class
{
    protected FunctionLog Log { get; }

    private ILogger _logger;
    public readonly IValidator? _validator;

    private readonly string _functionName;

    private CancellationToken? _cancellationToken;

    public CancellationToken CancellationToken
    {
        get
        {
            _cancellationToken ??= default;

            return _cancellationToken.Value;
        }
    }

    protected FunctionBase(ILogger logger)
    {
        _logger = logger;
        _functionName = typeof(T).Name;
        Log = new FunctionLog(logger, _functionName);
    }

    protected FunctionBase(ILogger logger, IValidator validator)
    {
        _logger = logger;
        _functionName = typeof(T).Name;
        Log = new FunctionLog(logger, _functionName);
        _validator = validator;
    }

    protected async Task ExecuteAsync<TCommand>(string deserializedCommand, FunctionContext context) where TCommand : ICommand
    {
        try
        {
            using (Init(context, deserializedCommand, out TCommand command))
            {
                await ThrowInvalid(command);

                await command.ExecuteAsync(CancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "<{functionName:l}> Execution failed for command {@deserializedCommand}", _functionName, deserializedCommand);
            throw;
        }
    }

    protected async Task ExecuteAsync<TCommand>(ICommand command, FunctionContext context) where TCommand : ICommand
    {
        try
        {
            using (Init(context))
            {
                var commandInstance = (TCommand)command;

                await ThrowInvalid(command);

                await commandInstance.ExecuteAsync(CancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "<{functionName:l}> Execution failed for command {@command}", _functionName, command);
            throw;
        }
    }

    private async Task ThrowInvalid<TCommand>(TCommand command) where TCommand : ICommand
    {
        if (_validator is null)
        {
            return;
        }

        var validationContext = new FluentValidation.ValidationContext<ICommand>(command);
        var validationResult = await _validator.ValidateAsync(validationContext);

        if (!validationResult.IsValid)
        {
            throw new ValidationException($"<{_functionName}> Validation failed for command {@command}", validationResult.Errors);
        }
    }

    private IDisposable Init<TMessage>(FunctionContext context, string json, out TMessage message)
    {
        _logger = Log.SetInvocationId(context.InvocationId);
        message = JsonSerializer.Deserialize<TMessage>(json, JsonOptions.Defaults)!;
        return _logger.TimeOperation("<{FunctionName:l}> Executing function for {@Message}", _functionName, message);
    }

    private IDisposable Init(FunctionContext context)
    {
        _logger = Log.SetInvocationId(context.InvocationId);
        return _logger.TimeOperation("<{FunctionName:l}> Executing function", _functionName);
    }
}