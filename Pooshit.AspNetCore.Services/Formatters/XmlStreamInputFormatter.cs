using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters;

/// <summary>
/// formatter accepting streams with application/xml media type
/// </summary>
public class XmlStreamInputFormatter : InputFormatter {

    /// <summary>
    /// creates a new <see cref="XmlStreamInputFormatter"/>
    /// </summary>
    public XmlStreamInputFormatter() {
        SupportedMediaTypes.Add("application/xml");
    }

    /// <inheritdoc />
    protected override bool CanReadType(Type type) {
        return typeof(XDocument).IsAssignableFrom(type);
    }

    /// <inheritdoc />
    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context) {
        return await InputFormatterResult.SuccessAsync(await XDocument.LoadAsync(context.HttpContext.Request.Body, LoadOptions.None, CancellationToken.None));
    }
}