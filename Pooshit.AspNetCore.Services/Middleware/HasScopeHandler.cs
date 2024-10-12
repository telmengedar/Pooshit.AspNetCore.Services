using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pooshit.AspNetCore.Services.Errors.Exceptions;

namespace Pooshit.AspNetCore.Services.Middleware {

    /// <summary>
    /// authorization handler which checks the required scope of a request
    /// </summary>
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement> {

        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement) {

            // Split the scopes string into an array
            string[] scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)?.Value.Split(' ') ?? new string[0];

            // Succeed if the scope array contains the required scope
            if(scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);
            else {
                if(!context.User.Claims.Any())
                    throw new NoClaimsException("The specified token is not valid");
                throw new MissingScopeException(requirement.Scope);
            }

            return Task.CompletedTask;
        }
    }
}