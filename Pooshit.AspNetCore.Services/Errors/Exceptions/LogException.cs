using System;
using System.Collections.Generic;

namespace Pooshit.AspNetCore.Services.Errors.Exceptions;

/// <summary>
/// exception with additional structure information for structured logging
/// </summary>
public class LogException : Exception {
    readonly Dictionary<string, object> errorStructure;

    /// <summary>
    /// creates a new <see cref="LogException"/>
    /// </summary>
    /// <param name="message">message for exception</param>
    /// <param name="innerException">wrapped exception</param>
    /// <param name="errorStructure">error information structure for log</param>
    public LogException(string message, Exception innerException, Dictionary<string, object> errorStructure) 
        : base(message, innerException) {
        this.errorStructure = errorStructure;
    }

    /// <summary>
    /// error information structure for log
    /// </summary>
    public Dictionary<string, object> ErrorStructure => errorStructure;
}