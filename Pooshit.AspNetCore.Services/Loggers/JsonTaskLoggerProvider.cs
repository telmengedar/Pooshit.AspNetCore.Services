using Microsoft.Extensions.Logging;

namespace Pooshit.AspNetCore.Services.Loggers;

/// <summary>
/// provides logger for mamgo services
/// </summary>
public class JsonTaskLoggerProvider : ILoggerProvider {
        
    /// <inheritdoc />
    public void Dispose() {
    }

    /// <inheritdoc />
    public ILogger CreateLogger(string categoryName) {
        return new JsonTaskLogger(categoryName);
    }
}