namespace Pooshit.AspNetCore.Services.Data; 

/// <summary>
/// basic filter for list operations
/// </summary>
public class ListFilter : PageFilter {
        
    /// <summary>
    /// property to sort result by
    /// </summary>
    public string Sort { get; set; }

    /// <summary>
    /// indicates whether to sort ascending or descending
    /// </summary>
    public bool Descending { get; set; }

    /// <summary>
    /// query which specifies a generic filter to apply
    /// </summary>
    public string Query { get; set; }

    /// <summary>
    /// specifies fields to load
    /// </summary>
    public string[] Fields { get; set; }

    /// <summary>
    /// determines whether to randomize the list result
    /// </summary>
    /// <summary>used to get a set of samples, makes no sense to combine with paging operations</summary>
    public bool? Randomize { get; set; }
}