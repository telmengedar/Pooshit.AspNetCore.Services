using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters;

/// <summary>
/// provides raw streams as output
/// </summary>
public class StreamOutputFormatter : OutputFormatter {
        
    /// <inheritdoc />
    public override IReadOnlyList<string> GetSupportedContentTypes(string contentType, Type objectType) {
        return new[] {
                         "*/*"
                     };
    }

    /// <inheritdoc />
    public override bool CanWriteResult(OutputFormatterCanWriteContext context) {
        return context.ObjectType == typeof(Stream);
    }

    /// <inheritdoc />
    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
        if(context.Object == null)
            return Task.CompletedTask;
            
        if (typeof(Stream).IsAssignableFrom(context.ObjectType))
            return ((Stream) context.Object).CopyToAsync(context.HttpContext.Response.Body);
            
        return Task.CompletedTask;
    }
}