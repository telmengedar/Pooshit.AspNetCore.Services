using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Errors.Handlers;

/// <inheritdoc />
public class PropertyNotFoundHandler : ErrorHandler<PropertyNotFoundException> {

    /// <summary>
    /// creates a new <see cref="PropertyNotFoundHandler"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    public PropertyNotFoundHandler(ILogger<PropertyNotFoundHandler> logger)
        : base(logger)
    {
    }

    /// <inheritdoc />
    protected override void LogError(ILogger errorlogger, PropertyNotFoundException error, HttpContext context) {
        errorlogger.LogInformation(error, "property '{Property}' of '{Type}' not found", error.PropertyName, error.DataType);
    }

    /// <inheritdoc />
    protected override HttpStatusCode HttpStatus => HttpStatusCode.NotFound;

    /// <inheritdoc />
    protected override ErrorResponse GenerateResponse(PropertyNotFoundException exception, HttpContext context) {
        return new(DefaultErrorCodes.DataPropertyNotFound, exception.Message);
    }
}