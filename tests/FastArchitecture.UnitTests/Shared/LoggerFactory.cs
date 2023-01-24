﻿namespace FastArchitecture.UnitTests.Shared;

public static class LoggerFactory
{
    public static ILogger Create()
    {
        var logger = new LoggerConfiguration()
            .CreateLogger();

        Log.Logger ??= logger;

        return logger;
    }
}