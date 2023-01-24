using FastArchitecture.Functions.Configuration;
using FastArchitecture.Functions.Logging;
using Microsoft.Azure.Functions.Worker;
using Serilog;
using SerilogTimings.Extensions;
using System.Text.Json;

namespace FastArchitecture.Functions.Abstractions;

public abstract class FunctionBase<T> where T : class
{
    protected FunctionLog Log { get; }

    private ILogger _logger;
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

    protected async Task<TResult> ExecuteAsync<TMessage, TResult>(FunctionContext context, string json, Func<TMessage, Task<TResult>> func) where TMessage : class
    {
        try
        {
            using (Init(context, json, out TMessage message))
            {
                return await func.Invoke(message);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "<{FunctionName:l}> Execution failed for input {@Json}", _functionName, json);
            throw;
        }
    }

    protected async Task ExecuteAsync<TMessage>(FunctionContext context, string json, Func<TMessage, Task> func) where TMessage : class
    {
        try
        {
            using (Init(context, json, out TMessage message))
            {
                await func.Invoke(message);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "<{FunctionName:l}> Execution failed for input {Json}", _functionName, json);
            throw;
        }
    }

    protected async Task ExecuteAsync(FunctionContext context, Func<Task> func)
    {
        try
        {
            using (Init(context))
            {
                await func.Invoke();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error");
            throw;
        }
    }

    protected async Task<TResponse> ExecuteAsync<TResponse>(FunctionContext context, Func<Task<TResponse>> func)
    {
        try
        {
            using (Init(context))
            {
                return await func.Invoke();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error");
            throw;
        }
    }

    protected TResult Execute<TMessage, TResult>(FunctionContext context, string json, Func<TMessage, TResult> func) where TMessage : class
    {
        try
        {
            using (Init(context, json, out TMessage message))
            {
                return func.Invoke(message);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "<{FunctionName:l}> Execution failed for input {Json}", _functionName, json);
            throw;
        }
    }

    protected void Execute<TMessage>(FunctionContext context, string json, Action<TMessage> func) where TMessage : class
    {
        try
        {
            using (Init(context, json, out TMessage message))
            {
                func.Invoke(message);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "<{FunctionName:l}> Execution failed for input {Json}", _functionName, json);
            throw;
        }
    }

    protected void Execute(FunctionContext context, Action func)
    {
        try
        {
            using (Init(context))
            {
                func.Invoke();
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error");
            throw;
        }
    }

    private IDisposable Init<TMessage>(FunctionContext context, string json, out TMessage message) where TMessage : class
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