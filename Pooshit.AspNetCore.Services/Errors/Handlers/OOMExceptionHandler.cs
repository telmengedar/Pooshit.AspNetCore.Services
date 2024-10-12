using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Errors.Handlers;

/// <summary>
/// handler for out of memory exceptions
/// </summary>
public class OOMExceptionHandler : ErrorHandler<OutOfMemoryException> {
    
    /// <summary>
    /// creates a new <see cref="OOMExceptionHandler"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    public OOMExceptionHandler(ILogger<OOMExceptionHandler> logger) : base(logger) { }

    /// <inheritdoc />
    protected override ErrorResponse GenerateResponse(OutOfMemoryException exception, HttpContext context) {
        return new() {
                         Code = "system_oom",
                         Text = "Out of Memory"
                     };
    }

    /// <inheritdoc />
    protected override HttpStatusCode HttpStatus => HttpStatusCode.InternalServerError;

    /// <inheritdoc />
    protected override void LogError(ILogger errorlogger, OutOfMemoryException error, HttpContext context) {
        errorlogger.LogCritical(new LogException("Out of Memory", error, new() {
                                                                                   ["code"] = DefaultErrorCodes.OOM,
                                                                                   ["text"] = "Out of Memory",
                                                                                   ["context"] = new Dictionary<string, object> {
                                                                                                                                    ["path"] = context.Request.GetDisplayUrl(),
                                                                                                                                    ["stack"] = error.StackTrace
                                                                                                                                }
                                                                               }), "Out of Memory");
    }
}