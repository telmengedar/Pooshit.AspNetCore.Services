using System.Collections.Generic;
using System.Threading.Tasks;
using Pooshit.Json.Writer;

namespace Pooshit.AspNetCore.Services.Formatters.DataStream; 

/// <summary>
/// writes an array of json data directly to a stream
/// </summary>
/// <typeparam name="T">type of data to write</typeparam>
public class AsyncArrayResponseWriter<T> : ResponseWriter {
    readonly IAsyncEnumerable<T> data;

    /// <summary>
    /// creates a new <see cref="AsyncArrayResponseWriter{T}"/>
    /// </summary>
    /// <param name="data">data to write</param>
    public AsyncArrayResponseWriter(IAsyncEnumerable<T> data) {
        this.data = data;
    }

    /// <inheritdoc />
    protected override async Task WriteJson(JsonStreamWriter writer) {
        await writer.BeginArrayAsync();
        await foreach (T item in data)
            await writer.WriteValueAsync(item);
        await writer.EndArrayAsync();
    }
}