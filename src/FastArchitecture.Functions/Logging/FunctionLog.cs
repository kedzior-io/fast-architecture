using Serilog;

namespace FastArchitecture.Functions.Logging;

/// <summary>
/// An ILogger wrapper with a function-specific enriched context and log message templating.
/// </summary>
public class FunctionLog
{
    private ILogger _logger;
    private readonly string _functionName;

    public FunctionLog(ILogger logger, string functionName)
    {
        _logger = logger;
        _functionName = functionName;
    }

    public ILogger SetInvocationId(string id)
    {
        _logger = _logger.ForContext("InvocationId", id);
        return _logger;
    }

    public void Information(string messageTemplate, params object[] propertyValues)
    {
        messageTemplate = string.Concat("<{FunctionName:l}> ", messageTemplate);

        switch (propertyValues.Length)
        {
            case 1:
                _logger.Information(messageTemplate, _functionName, propertyValues[0]);
                break;

            case 2:
                _logger.Information(messageTemplate, _functionName, propertyValues[0], propertyValues[1]);
                break;

            case 3:
                _logger.Information(messageTemplate, _functionName, propertyValues[0], propertyValues[1], propertyValues[2]);
                break;

            default:
                _logger.Information(messageTemplate, _functionName, propertyValues);
                break;
        }
    }

    public void Warning(string messageTemplate, params object[] propertyValues)
    {
        messageTemplate = string.Concat("<{FunctionName:l}> ", messageTemplate);

        switch (propertyValues.Length)
        {
            case 1:
                _logger.Warning(messageTemplate, _functionName, propertyValues[0]);
                break;

            case 2:
                _logger.Warning(messageTemplate, _functionName, propertyValues[0], propertyValues[1]);
                break;

            case 3:
                _logger.Warning(messageTemplate, _functionName, propertyValues[0], propertyValues[1], propertyValues[2]);
                break;

            default:
                _logger.Warning(messageTemplate, _functionName, propertyValues);
                break;
        }
    }

    public void Error(string messageTemplate, params object[] propertyValues)
    {
        _logger.Error(string.Concat("<{FunctionName:l}> ", messageTemplate), _functionName, propertyValues);
    }

    public void Error(Exception exception)
    {
        _logger.Error(exception, string.Concat("<{FunctionName:l}> ", exception.Message), _functionName);
    }
}