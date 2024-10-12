using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Loggers;

/// <summary>
/// logger for mamgo log output
/// </summary>
public class JsonLogger : ILogger {
    static readonly string[] levels = ["DEBUG", "DEBUG", "INFO", "WARNING", "ERROR", "CRITICAL", "DEFAULT"];
    readonly string category;
            
    /// <summary>
    /// creates a new <see cref="SingleLineLogger"/>
    /// </summary>
    /// <param name="category">category for which logger is created</param>
    public JsonLogger(string category) {
        this.category = category.Split('.').LastOrDefault();
    }

    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
        if (!IsEnabled(logLevel))
            return;

        if (exception is LogException logException) {
            Console.WriteLine(Json.Json.WriteString(new Dictionary<string, object> {
                                                                                       ["severity"] = levels[(int)logLevel],
                                                                                       ["time"] = DateTime.UtcNow,
                                                                                       ["module"] = category,
                                                                                       ["message"] = formatter(state, null),
                                                                                       ["error"] = logException.ErrorStructure,
                                                                                       ["exception"] = logException.InnerException?.ToString()
                                                                                   }));
        }
        else {
            Console.WriteLine(Json.Json.WriteString(new Dictionary<string, object> {
                                                                                       ["severity"] = levels[(int)logLevel],
                                                                                       ["time"] = DateTime.UtcNow,
                                                                                       ["module"] = category,
                                                                                       ["message"] = formatter(state, null),
                                                                                       ["exception"] = exception?.ToString()
                                                                                   }));
        }
    }

    /// <inheritdoc />
    public bool IsEnabled(LogLevel logLevel) {
        return true;
    }

    /// <inheritdoc />
    public IDisposable BeginScope<TState>(TState state) {
        return null;
    }
}