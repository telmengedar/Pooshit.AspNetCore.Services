using Microsoft.AspNetCore.Authorization;

namespace Pooshit.AspNetCore.Services.Middleware {


    /// <summary>
    /// requires a user to have a scope to continue
    /// </summary>
    public class HasScopeRequirement : IAuthorizationRequirement {

        /// <summary>
        /// creates a new <see cref="HasScopeRequirement"/>
        /// </summary>
        /// <param name="issuer">required issuer of scope</param>
        /// <param name="scope">required scope name</param>
        public HasScopeRequirement(string issuer, string scope) {
            Issuer = issuer;
            Scope = scope;
        }

        /// <summary>
        /// required issuer of scope
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// required scope name
        /// </summary>
        public string Scope { get; set; }

    }
}