using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Handlers;
using Pooshit.AspNetCore.Services.Formatters;
using Pooshit.Json;

namespace Pooshit.AspNetCore.Services.Errors;

/// <summary>
/// handles errors of a http request
/// </summary>
public class ErrorHandlerCollection : IErrorHandlerCollection {
    readonly ILogger logger;
    readonly Dictionary<Type, IErrorHandler> handlers = new();

    /// <summary>
    /// creates a new <see cref="ErrorHandlerCollection"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    /// <param name="errorhandlers">registered error handlers</param>
    public ErrorHandlerCollection(ILogger<ErrorHandlerCollection> logger, IEnumerable<IErrorHandler> errorhandlers) {
        this.logger = logger;
        foreach(IErrorHandler handler in errorhandlers)
            handlers[handler.ExceptionType] = handler;
    }

    /// <inheritdoc />
    public Task HandleError(Exception error, HttpResponse response, bool responseavailable) {
        if(handlers.TryGetValue(error.GetType(), out IErrorHandler handler))
            return handler.HandleError(error, response, responseavailable);
        return DefaultErrorHandler(error, response, responseavailable);
    }

    async Task DefaultErrorHandler(Exception error, HttpResponse response, bool responseavailable) {
        logger.LogError(error, "Unhandled exception encountered");

        if (responseavailable) {
            response.StatusCode = 500;
            response.ContentType = "application/json";
            await using StreamWriter writer = new(response.Body);
            await Json.Json.WriteAsync(new ErrorResponse(DefaultErrorCodes.Unhandled, error.Message), writer, JsonOptions.RestApi);
        }
    }
}