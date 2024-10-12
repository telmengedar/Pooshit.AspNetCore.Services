using Microsoft.Extensions.DependencyInjection;
using Pooshit.AspNetCore.Services.Errors;
using Pooshit.AspNetCore.Services.Errors.Handlers;

namespace Pooshit.AspNetCore.Services.Extensions;

/// <summary>
/// extensions used for error handling
/// </summary>
public static class ErrorExtensions {

    /// <summary>
    /// adds error handling aswell as default error handlers to the service collection
    /// </summary>
    /// <param name="services">service collection to add services to</param>
    /// <returns>service collection for fluent behavior</returns>
    public static IServiceCollection AddErrorHandlers(this IServiceCollection services) {
        services.AddSingleton<IErrorHandlerCollection, ErrorHandlerCollection>();
        services.AddSingleton<IErrorHandler, MissingScopeHandler>();
        services.AddSingleton<IErrorHandler, NoClaimsHandler>();
        services.AddSingleton<IErrorHandler, DataNotFoundHandler>();
        services.AddSingleton<IErrorHandler, PropertyNotFoundHandler>();
        services.AddSingleton<IErrorHandler, OOMExceptionHandler>();
        return services;
    }
}