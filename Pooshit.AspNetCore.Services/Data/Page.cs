namespace Pooshit.AspNetCore.Services.Data;

/// <summary>
/// result of a listing request
/// </summary>
/// <typeparam name="TData">type of result data</typeparam>
public class Page<TData> {

    /// <summary>
    /// result data
    /// </summary>
    public TData[] Result { get; set; }

    /// <summary>
    /// token to use when continuing listings
    /// </summary>
    /// <remarks>
    /// if this is not set, no more pages are available
    /// </remarks>
    public long? Continue { get; set; }

    /// <summary>
    /// number of total results this page is based on
    /// </summary>
    public long Total { get; set; }

    /// <summary>
    /// creates a page result with a continue token
    /// only fills the continue token if length of page result is equal to maxlength
    /// </summary>
    /// <param name="data">data to use as result</param>
    /// <param name="token">continue token to use</param>
    /// <param name="total">number of total results the page is based on</param>
    /// <returns>created page result</returns>
    public static Page<TData> Create(TData[] data, long total, long? token = null) {
        Page<TData> page = new() {
                                     Result = data,
                                     Total = total
                                 };

        if (token.HasValue) {
            if(token + data.Length < total)
                page.Continue = token + data.Length;
        }
        else {
            if (data.Length < total)
                page.Continue = data.Length;
        }

        return page;
    }

    /// <summary>
    /// generates an empty page
    /// </summary>
    public static Page<TData> Empty => new() {
                                                 Result = []
                                             };
}