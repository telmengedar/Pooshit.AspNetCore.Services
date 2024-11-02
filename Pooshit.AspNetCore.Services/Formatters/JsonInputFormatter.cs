using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters;

/// <summary>
/// input formatter using utf8-json to deserialize models
/// </summary>
public class JsonInputFormatter : InputFormatter {
        
    /// <summary>
    /// creates a new <see cref="JsonInputFormatter"/>
    /// </summary>
    public JsonInputFormatter() {
        SupportedMediaTypes.Add("application/json");
        SupportedMediaTypes.Add("text/json");
        SupportedMediaTypes.Add("application/*+json");
    }

    /// <summary>
    /// use full async operations
    /// </summary>
    public bool Async { get; set; }
        
    /// <inheritdoc />
    protected override bool CanReadType(Type type) {
        return true;
    }

    /// <inheritdoc />
    public override Task<InputFormatterResult> ReadAsync(InputFormatterContext context) {
        return ReadRequestBodyAsync(context);
    }

    /// <inheritdoc />
    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context) {
        if (Async)
            return await InputFormatterResult.SuccessAsync(await Json.Json.ReadAsync(context.ModelType, context.HttpContext.Request.Body));
            
        await using MemoryStream memory=new();
        await context.HttpContext.Request.Body.CopyToAsync(memory);
        memory.Position = 0;
        // ReSharper disable once MethodHasAsyncOverload
        object model = Json.Json.Read(context.ModelType, memory);
        return await InputFormatterResult.SuccessAsync(model);
    }
}