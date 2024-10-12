using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pooshit.AspNetCore.Services.Formatters.DataStream;
using Pooshit.AspNetCore.Services.Loggers;
using Pooshit.AspNetCore.Services.Middleware;

namespace Pooshit.AspNetCore.Services.Extensions; 

/// <summary>
/// extension methods for webapplicationbuilder
/// </summary>
public static class WebApplicationBuilderExtensions {
    
    /// <summary>
    /// starts a basic mamgo service
    /// </summary>
    /// <param name="builder">builder to use</param>
    /// <param name="services">action used to initialize services</param>
    /// <param name="configureMvc">additional mvc configuration</param>
    /// <param name="application">action used to initialize application</param>
    public static void StartDefault(this WebApplicationBuilder builder, Action<IServiceCollection> services = null, Action<MvcOptions> configureMvc=null, Action<WebApplication> application = null) {
        builder.Services.AddErrorHandlers();
        builder.Services.AddLogging(options => {
            options.ClearProviders();
            options.AddProvider(new SingleLineLoggerProvider());
        });
        builder.Services.AddSingleton<IConfigureOptions<MvcOptions>, MvcConfiguration>();
        builder.Services.ConfigureMvc(options => {
            options.OutputFormatters.Insert(0, new DataStreamOutputFormatter());
            configureMvc?.Invoke(options);
        });
        builder.Services.AddControllers();
        services?.Invoke(builder.Services);

        WebApplication app = builder.Build();
        app.UseRouting();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.MapControllers();
        application?.Invoke(app);
        app.Run();
    }
}