using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pooshit.AspNetCore.Services.Errors;
using Pooshit.AspNetCore.Services.Formatters;

namespace Pooshit.AspNetCore.Services.Middleware;

/// <summary>
/// default mvc configuration for services
/// </summary>
public class MvcConfiguration : IConfigureOptions<MvcOptions> {
    readonly ILogger<MvcConfiguration> logger;
    readonly IConfiguration configuration;

    /// <summary>
    /// creates a new <see cref="MvcConfiguration"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    /// <param name="configuration">access to service configuration</param>
    public MvcConfiguration(ILogger<MvcConfiguration> logger, IConfiguration configuration) {
        this.logger = logger;
        this.configuration = configuration;
    }

    /// <inheritdoc />
    public void Configure(MvcOptions options) {
        options.InputFormatters.Clear();
        options.OutputFormatters.Clear();
        options.MaxValidationDepth = null;

        bool asyncjson = configuration["Json:Async"]?.ToLower() != "false";
        options.Filters.Add(new ValidateRequirementsAttribute(logger));
        options.InputFormatters.Add(new StreamInputFormatter());
        options.InputFormatters.Add(new NCJsonInputFormatter {
                                                                 Async = asyncjson
                                                             });

        options.OutputFormatters.Add(new StreamOutputFormatter());
        options.OutputFormatters.Add(new HttpResponseOutputFormatter());
        options.OutputFormatters.Add(new NCJsonOutputFormatter {
                                                                   Async = asyncjson
                                                               });
    }
}