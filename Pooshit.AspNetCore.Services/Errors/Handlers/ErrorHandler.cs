using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Formatters;
using Pooshit.Json;

namespace Pooshit.AspNetCore.Services.Errors.Handlers;

/// <summary>
/// base class for error handlers
/// </summary>
public abstract class ErrorHandler<T> : IErrorHandler
    where T : Exception
{
    readonly ILogger logger;

    /// <summary>
    /// creates a new <see cref="ErrorHandler{T}"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    protected ErrorHandler(ILogger logger) {
        this.logger = logger;
    }

    /// <inheritdoc />
    public Type ExceptionType => typeof(T);

    /// <summary>
    /// status code written to the response
    /// </summary>
    protected abstract HttpStatusCode HttpStatus { get; }
        
    /// <summary>
    /// generates a error response using exception and http context
    /// </summary>
    /// <param name="exception">exception for which to </param>
    /// <param name="context">http context for request in which error occured</param>
    /// <returns>error response sent to client</returns>
    protected virtual ErrorResponse GenerateResponse(T exception, HttpContext context) {
        return new() {
                         Code = DefaultErrorCodes.Unhandled,
                         Text = exception.Message
                     };
    }

    /// <summary>
    /// called when error is to be logged
    /// </summary>
    /// <param name="errorlogger">logger to use</param>
    /// <param name="error">error to log</param>
    /// <param name="context">http context for request in which error occured</param>
    protected virtual void LogError(ILogger errorlogger, T error, HttpContext context) {
        logger.LogError("{Message}", error.Message);
    }

    /// <inheritdoc />
    public Task HandleError(Exception error, HttpResponse response, bool responseavailable) {
        LogError(logger, (T) error, response.HttpContext);

        if (responseavailable) {
            response.StatusCode = (int) HttpStatus;
            response.ContentType = "application/json";
            return Json.Json.WriteAsync(GenerateResponse((T) error, response.HttpContext), response.Body, JsonOptions.RestApi);
        }

        return Task.CompletedTask;
    }
}