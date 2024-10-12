using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters.DataStream;

/// <summary>
/// output formatter for streamed json
/// </summary>
public class DataStreamOutputFormatter : OutputFormatter {
        
    /// <summary>
    /// creates a new <see cref="DataStreamOutputFormatter"/>
    /// </summary>
    public DataStreamOutputFormatter() {
        SupportedMediaTypes.Add("application/json");
        SupportedMediaTypes.Add("application/xml");
    }

    /// <inheritdoc />
    protected override bool CanWriteType(Type type) {
        return typeof(IResponseWriter).IsAssignableFrom(type);
    }

    /// <inheritdoc />
    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
        IResponseWriter response = (IResponseWriter)context.Object;
        if (response == null)
            return Task.CompletedTask;
            
        context.HttpContext.Response.ContentType = response.ContentType;
        return response.Write(context.HttpContext.Response.Body);
    }
}