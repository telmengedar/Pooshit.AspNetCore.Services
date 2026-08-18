using System.IO;
using System.Threading.Tasks;
using Pooshit.Json.Writer;

namespace Pooshit.AspNetCore.Services.Formatters.DataStream; 

/// <summary>
/// writer used to write json directly to stream without buffering data
/// </summary>
public abstract class ResponseWriter : IResponseWriter {
    
    /// <inheritdoc />
    public virtual string ContentType => "application/json";

    /// <summary>
    /// writes data to the stream
    /// </summary>
    /// <param name="target">stream to write data to</param>
    public async Task Write(Stream target) {
        await using JsonStreamWriter streamWriter = new(target, Json.JsonOptions.RestApi);
        await WriteJson(streamWriter);
    }

    /// <summary>
    /// writes response data
    /// </summary>
    /// <param name="writer">writer to use to write json data</param>
    protected abstract Task WriteJson(JsonStreamWriter writer);
}