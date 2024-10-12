using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pooshit.AspNetCore.Services.Middleware;

namespace Pooshit.AspNetCore.Services.Extensions {

    /// <summary>
    /// extensions for authorization
    /// </summary>
    public static class AuthorizationExtensions {

        /// <summary>
        /// creates policies from scopes defined in a scope container class
        /// </summary>
        /// <param name="mvc">service collection to add authorization information to</param>
        /// <param name="domain">domain for which to add authorization</param>
        /// <param name="scopecontainer">type containing scopes</param>
        public static void AddPoliciesFromScopes(this IMvcCoreBuilder mvc, string domain, Type scopecontainer) {
            mvc.AddAuthorization(o => {
                foreach(FieldInfo field in scopecontainer.GetFields()) {
                    if(field.GetValue(null) is string scope)
                        o.AddPolicy(scope, p => p.Requirements.Add(new HasScopeRequirement(domain, scope)));
                }
            });
        }
    }
}