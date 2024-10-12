namespace Pooshit.AspNetCore.Services.Data; 

/// <summary>
/// filter used for page listings
/// </summary>
public class PageFilter {
        
    /// <summary>
    /// number of jobs to return
    /// </summary>
    /// <remarks>
    /// limits to 500. Even if more than 500 is specified only 500 results are returned
    /// </remarks>
    public long? Count { get; set; }

    /// <summary>
    /// token to use to get a following page in a listing result
    /// </summary>
    public long? Continue { get; set; }

    /// <summary>
    /// maximum value which is valid for count field
    /// </summary>
    public const long MaxCount = 500;
}