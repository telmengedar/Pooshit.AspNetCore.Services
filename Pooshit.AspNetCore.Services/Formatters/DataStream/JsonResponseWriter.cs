using System;
using System.Threading.Tasks;
using Pooshit.Json;
using Pooshit.Json.Writer;

namespace Pooshit.AspNetCore.Services.Formatters.DataStream;

/// <summary>
/// writer used to write json directly to stream without buffering data
/// </summary>
public class JsonResponseWriter : IResponseWriter {
    readonly Func<JsonStreamWriter, Task> outputOperation;

    /// <summary>
    /// creates a new <see cref="JsonResponseWriter"/>
    /// </summary>
    /// <param name="outputOperation">operation used to write to stream</param>
    public JsonResponseWriter(Func<JsonStreamWriter, Task> outputOperation) {
        this.outputOperation = outputOperation;
    }

    /// <inheritdoc />
    public virtual string ContentType => "application/json";

    /// <summary>
    /// writes data to the stream
    /// </summary>
    /// <param name="target">stream to write data to</param>
    public async Task Write(System.IO.Stream target) {
        await using JsonStreamWriter streamWriter = new(target, JsonOptions.RestApi);
        await outputOperation(streamWriter);
    }
}