using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Errors.Handlers;

/// <summary>
/// handles <see cref="NoClaimsException"/>s
/// </summary>
public class NoClaimsHandler : ErrorHandler<NoClaimsException> {

    /// <summary>
    /// creates a new <see cref="NoClaimsHandler"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    public NoClaimsHandler(ILogger<NoClaimsHandler> logger)
        : base(logger)
    {
    }

    /// <inheritdoc />
    protected override void LogError(ILogger errorlogger, NoClaimsException error, HttpContext context) {
        errorlogger.LogWarning(error, "Invalid token");
    }

    /// <inheritdoc />
    protected override HttpStatusCode HttpStatus => HttpStatusCode.Forbidden;

    /// <inheritdoc />
    protected override ErrorResponse GenerateResponse(NoClaimsException exception, HttpContext context) {
        return new(DefaultErrorCodes.InvalidToken, exception.Message);
    }
}