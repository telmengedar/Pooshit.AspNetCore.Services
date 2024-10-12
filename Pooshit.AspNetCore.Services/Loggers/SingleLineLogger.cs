using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Pooshit.AspNetCore.Services.Loggers;

/// <summary>
/// logger for mamgo log output
/// </summary>
public class SingleLineLogger : ILogger {
    static readonly string[] levels = ["TRC", "DBG", "INF", "WRN", "ERR", "CRT", "NON"];
    readonly string category;
            
    /// <summary>
    /// creates a new <see cref="SingleLineLogger"/>
    /// </summary>
    /// <param name="category">category for which logger is created</param>
    public SingleLineLogger(string category) {
        this.category = category.Split('.').LastOrDefault();
    }

    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
        if (!IsEnabled(logLevel))
            return;
                
        Console.WriteLine($"{levels[(int) logLevel]} {category}: {state}");
        if (exception != null)
            Console.WriteLine(exception);
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