using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Errors.Handlers;

/// <inheritdoc />
public class DataNotFoundHandler : ErrorHandler<NotFoundException> {
        
    /// <summary>
    /// creates a new <see cref="DataNotFoundHandler"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    public DataNotFoundHandler(ILogger<DataNotFoundHandler> logger)
        : base(logger)
    {
    }

    /// <inheritdoc />
    protected override void LogError(ILogger errorlogger, NotFoundException error, HttpContext context) {
        errorlogger.LogInformation(error, "{type} with id {id} not found", error.DataType, error.Id);
    }

    /// <inheritdoc />
    protected override HttpStatusCode HttpStatus => HttpStatusCode.NotFound;

    /// <inheritdoc />
    protected override ErrorResponse GenerateResponse(NotFoundException exception, HttpContext context) {
        return new(DefaultErrorCodes.DataEntityNotFound, exception.Message);
    }
}