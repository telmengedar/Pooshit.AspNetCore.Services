using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pooshit.AspNetCore.Services.Data;
using Pooshit.Json.Writer;

namespace Pooshit.AspNetCore.Services.Formatters.DataStream;

/// <summary>
/// writes a page of json data directly to a stream
/// </summary>
/// <typeparam name="T">type of data to write</typeparam>
public class AsyncPageResponseWriter<T> : ResponseWriter {
    readonly IAsyncEnumerable<T> data;
    readonly Func<Task<long>> totalOp;
    readonly long? token;

    /// <summary>
    /// creates a new <see cref="AsyncPageResponseWriter{T}"/>
    /// </summary>
    /// <param name="data">data to write</param>
    /// <param name="totalOp">operation loading number of total results</param>
    /// <param name="token">token to use to continue paging</param>
    public AsyncPageResponseWriter(IAsyncEnumerable<T> data, Func<Task<long>> totalOp, long? token = null) {
        this.data = data;
        this.totalOp = totalOp;
        this.token = token;
    }

    /// <summary>
    /// generates classic page data from
    /// </summary>
    /// <returns>page object</returns>
    public async Task<Page<T>> ToPage() {
        return Page<T>.Create(await data.ToArrayAsync(),
                              await totalOp(),
                              token);
    }

    /// <inheritdoc />
    protected override async Task WriteJson(JsonStreamWriter writer) {
        long length = 0;
        await writer.BeginObjectAsync();
        await writer.WriteKeyAsync("result");
        await writer.BeginArrayAsync();
        await foreach (T item in data) {
            await writer.WriteValueAsync(item);
            ++length;
        }

        await writer.EndArrayAsync();

        long total = await totalOp();
        await writer.WritePropertyAsync("total", total);
        if (token.HasValue)
            length += token.Value;

        if (length < total)
            await writer.WritePropertyAsync("continue", length);

        await writer.EndObjectAsync();
    }
}