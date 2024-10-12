using System;

namespace Pooshit.AspNetCore.Services.Errors.Exceptions {

    /// <summary>
    /// exception indicating that a scope is missing in authentication token which is required to access a resource
    /// </summary>
    public class MissingScopeException : Exception {

        /// <summary>
        /// creates a new <see cref="MissingScopeException"/>
        /// </summary>
        /// <param name="scope">scope which is missing</param>
        public MissingScopeException(string scope)
            : base($"Scope '{scope}' is required to access this resource") {
            Scope = scope;
        }

        /// <summary>
        /// scope which was requested
        /// </summary>
        public string Scope { get; }
    }
}