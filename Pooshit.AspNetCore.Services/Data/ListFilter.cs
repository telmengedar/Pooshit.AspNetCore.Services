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
}