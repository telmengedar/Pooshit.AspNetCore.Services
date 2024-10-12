using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Loggers;

/// <summary>
/// logger for mamgo log output
/// </summary>
public class JsonTaskLogger : ILogger {
    readonly Queue<Dictionary<string, object>> messages = new();
    readonly object taskLock = new();
    Task logTask;
    
    static readonly string[] levels = ["DEBUG", "DEBUG", "INFO", "WARNING", "ERROR", "CRITICAL", "DEFAULT"];
    readonly string category;
    
    /// <summary>
    /// creates a new <see cref="SingleLineLogger"/>
    /// </summary>
    /// <param name="category">category for which logger is created</param>
    public JsonTaskLogger(string category) {
        this.category = category.Split('.').LastOrDefault();
    }

    Task LogTask() {
        while (true) {
            lock (taskLock) {
                if (!messages.TryDequeue(out Dictionary<string, object> message)) {
                    logTask = null;
                    return Task.CompletedTask;
                }

                Console.WriteLine(Json.Json.WriteString(message));
            }
        }
    }

    void LogMessage(Dictionary<string, object> message) {
        lock (taskLock) {
            messages.Enqueue(message);
            logTask ??= LogTask();
        }
    }
    
    /// <inheritdoc />
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
        if (!IsEnabled(logLevel))
            return;

        if (exception is LogException logException) {
            LogMessage(new() {
                                 ["severity"] = levels[(int)logLevel],
                                 ["time"] = DateTime.UtcNow,
                                 ["module"] = category,
                                 ["message"] = formatter(state, null),
                                 ["error"] = logException.ErrorStructure,
                                 ["exception"] = logException.InnerException?.ToString()
                             });
        }
        else {
            LogMessage(new() {
                                 ["severity"] = levels[(int)logLevel],
                                 ["time"] = DateTime.UtcNow,
                                 ["module"] = category,
                                 ["message"] = formatter(state, null),
                                 ["exception"] = exception?.ToString()
                             });
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