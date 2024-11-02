using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters;

/// <summary>
/// mediates http responses to clients
/// </summary>
public class HttpResponseOutputFormatter : OutputFormatter {
    readonly HashSet<string> defaultheaders= [
                                                 ..new[] {
                                                             "Access-Control-Allow-Origin",
                                                             "Transfer-Encoding",
                                                             "Date",
                                                             "Etag",
                                                             "Keep-Alive",
                                                             "Last-Modified",
                                                             "Server"
                                                         }
                                             ];

    /// <inheritdoc />
    public override IReadOnlyList<string> GetSupportedContentTypes(string contentType, Type objectType) {
        return new[] {
                         "*/*"
                     };
    }

    /// <inheritdoc />
    public override bool CanWriteResult(OutputFormatterCanWriteContext context) {
        return context.ObjectType == typeof(HttpResponseMessage);
    }

    /// <inheritdoc />
    public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
        if(context.Object == null)
            return Task.CompletedTask;

        HttpResponseMessage httpresponse = (HttpResponseMessage) context.Object;
        context.HttpContext.Response.StatusCode = (int)httpresponse.StatusCode;

        if (httpresponse.Content.Headers.ContentType != null)
            // this somehow crashes if content is null
            context.HttpContext.Response.ContentType = httpresponse.Content.Headers.ContentType.ToString();

        //context.HttpContext.Response.Headers.Clear();
        foreach (KeyValuePair<string, IEnumerable<string>> header in httpresponse.Headers) {
            if (defaultheaders.Contains(header.Key) || context.HttpContext.Response.Headers.ContainsKey(header.Key))
                continue;
            context.HttpContext.Response.Headers[header.Key]= header.Value.ToArray();
        }

        if (httpresponse.StatusCode != HttpStatusCode.NoContent)
            return httpresponse.Content.CopyToAsync(context.HttpContext.Response.Body);
        return Task.CompletedTask;
    }
}