using System.Xml;

namespace Pooshit.AspNetCore.Services.Extensions {

    /// <summary>
    /// extensions for <see cref="XmlWriter"/>s
    /// </summary>
    public static class XmlWriterExtensions {

        /// <summary>
        /// writes an element with a value wrapped in cdata
        /// </summary>
        /// <param name="writer">writer to use to write element</param>
        /// <param name="element">element to write</param>
        /// <param name="value">value to write</param>
        public static void WriteCDataElement(this XmlWriter writer, string element, string value) {
            if(string.IsNullOrEmpty(value))
                return;

            writer.WriteStartElement(element);
            writer.WriteCData(value);
            writer.WriteEndElement();
        }
    }
}