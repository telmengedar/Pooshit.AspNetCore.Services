using System;

namespace Pooshit.AspNetCore.Services.Errors.Exceptions {

    /// <summary>
    /// thrown when no claims were found in token but an authorized endpoint was requested
    /// </summary>
    public class NoClaimsException : Exception {

        /// <summary>
        /// creates a new <see cref="NoClaimsException"/>
        /// </summary>
        /// <param name="message">message for error to contain</param>
        public NoClaimsException(string message) : base(message) {
        }
    }
}