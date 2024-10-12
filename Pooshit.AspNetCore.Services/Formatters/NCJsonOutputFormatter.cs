using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Pooshit.Json;

namespace Pooshit.AspNetCore.Services.Formatters;

/// <summary>
/// output formatter for json using nightlycode json library which is hopefully faster than newtonsoft
/// </summary>
public class NCJsonOutputFormatter : OutputFormatter {
        
    /// <summary>
    /// creates a new <see cref="NCJsonOutputFormatter"/>
    /// </summary>
    public NCJsonOutputFormatter() {
        SupportedMediaTypes.Add("application/json");
        SupportedMediaTypes.Add("text/json");
        SupportedMediaTypes.Add("application/*+json");
    }

    /// <summary>
    /// use full async operations
    /// </summary>
    public bool Async { get; set; }

    /// <inheritdoc />
    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
        if (context.Object == null)
            return;

        context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
        if (Async) {
            await Json.Json.WriteAsync(context.Object, context.HttpContext.Response.Body, JsonOptions.RestApi);
        }
        else {
            // Synchroneous operations are disallowed. As long as json doesn't fully operate async
            // flawlessly we have to use a memorystream here
            await using MemoryStream memory = new();
            // ReSharper disable once MethodHasAsyncOverload
            Json.Json.Write(context.Object, memory, JsonOptions.RestApi);
            memory.Position = 0;
            await memory.CopyToAsync(context.HttpContext.Response.Body);
        }
        // ReSharper disable once MethodHasAsyncOverload
        //Json.Json.Write(context.Object, context.HttpContext.Response.Body, JsonSettings.Options);
    }
}