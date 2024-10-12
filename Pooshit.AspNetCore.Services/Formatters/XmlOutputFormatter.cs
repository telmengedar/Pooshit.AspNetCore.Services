using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Pooshit.AspNetCore.Services.Formatters {

    /// <summary>
    /// output formatter for json using utf8json library which is hopefully faster than newtonsoft
    /// </summary>
    public class XmlOutputFormatter : OutputFormatter {

        /// <summary>
        /// creates a new <see cref="XmlOutputFormatter"/>
        /// </summary>
        public XmlOutputFormatter() {
            SupportedMediaTypes.Add("application/xml");
        }

        /// <inheritdoc />
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context) {
            DataContractSerializer serializer = new DataContractSerializer(context.ObjectType);
            serializer.WriteObject(context.HttpContext.Response.Body, context.Object);
            return Task.CompletedTask;
        }
    }
}