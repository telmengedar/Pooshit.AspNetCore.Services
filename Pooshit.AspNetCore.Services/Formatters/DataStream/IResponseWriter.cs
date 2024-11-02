using System.Threading.Tasks;

namespace Pooshit.AspNetCore.Services.Formatters.DataStream;

/// <summary>
/// writer used to write json directly to stream without buffering data
/// </summary>
public interface IResponseWriter {

    /// <summary>
    /// content type to write to reponse
    /// </summary>
    public string ContentType { get; }
        
    /// <summary>
    /// writes data to the stream
    /// </summary>
    /// <param name="target">stream to write data to</param>
    Task Write(System.IO.Stream target);
}