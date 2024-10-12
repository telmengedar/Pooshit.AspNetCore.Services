using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters {

    /// <summary>
    /// accepts zip streams for uploads
    /// </summary>
    public class ZipStreamInputFormatter : InputFormatter {

        /// <summary>
        /// creates a new <see cref="ZipStreamInputFormatter"/>
        /// </summary>
        public ZipStreamInputFormatter() {
            SupportedMediaTypes.Add("application/zip");
        }

        /// <inheritdoc />
        protected override bool CanReadType(Type type) {
            return type == typeof(Stream);
        }

        /// <inheritdoc />
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context) {
            // upload is buffered, else the code sometimes breaks with broken pipes
            // ... actually it always breaks ...
            MemoryStream buffer = new MemoryStream();
            await context.HttpContext.Request.Body.CopyToAsync(buffer);
            buffer.Position = 0;
            return await InputFormatterResult.SuccessAsync(buffer);
        }

    }
}