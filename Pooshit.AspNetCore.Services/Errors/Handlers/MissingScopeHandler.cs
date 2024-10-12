using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Errors.Handlers {

    /// <summary>
    /// handles <see cref="MissingScopeException"/>s
    /// </summary>
    public class MissingScopeHandler : ErrorHandler<MissingScopeException> {

        /// <summary>
        /// creates a new <see cref="MissingScopeHandler"/>
        /// </summary>
        /// <param name="logger">access to logging</param>
        public MissingScopeHandler(ILogger<MissingScopeHandler> logger)
        : base(logger)
        {
        }

        /// <inheritdoc />
        protected override HttpStatusCode HttpStatus => HttpStatusCode.Forbidden;

        /// <inheritdoc />
        protected override ErrorResponse GenerateResponse(MissingScopeException exception, HttpContext context) {
            return new(DefaultErrorCodes.MissingScope, exception.Message);
        }

        /// <inheritdoc />
        protected override void LogError(ILogger errorlogger, MissingScopeException error, HttpContext context) {
            errorlogger.LogWarning("Scope '{Scope}' was missing in a request", error.Scope);
        }
    }
}