using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters {
    
    /// <summary>
    /// provides streams to controllers
    /// </summary>
    public class StreamInputFormatter : InputFormatter {
        
        /// <inheritdoc />b
        public override IReadOnlyList<string> GetSupportedContentTypes(string contentType, Type objectType) {
            return new[] {
                "*/*"
            };
        }

        /// <inheritdoc />
        public override bool CanRead(InputFormatterContext context) {
            return context.ModelType == typeof(Stream);
        }

        /// <inheritdoc />
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context) {
            MemoryStream memory=new();
            await context.HttpContext.Request.Body.CopyToAsync(memory);
            memory.Position = 0;
            return await InputFormatterResult.SuccessAsync(memory);
        }
    }
}