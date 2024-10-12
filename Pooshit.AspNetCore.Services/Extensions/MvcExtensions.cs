using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Pooshit.AspNetCore.Services.Extensions {

    public static class MvcExtensions {

        public static IMvcCoreBuilder ConfigureMvc(this IServiceCollection services) {
            return ConfigureMvc(services, options => { });
        }

        public static IMvcCoreBuilder ConfigureMvc(this IServiceCollection services, Action<MvcOptions> options) {
            return services.AddMvcCore(options)
                .AddApiExplorer()
                .AddAuthorization(o => {
                    o.DefaultPolicy =
                        new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            //.RequireAssertion(ctx => ClaimsHelper.IsAdminUser(ctx.User.Claims.ToList()))
                            .Build();

                    /*options.AddPolicy(SecurityPolicyNames.DoesNotRequireAdminUser,
                        policy => policy.RequireAuthenticatedUser());*/
                });
        }
    }
}