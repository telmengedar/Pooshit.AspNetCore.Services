namespace Pooshit.AspNetCore.Services.Patches;

/// <summary>
/// provides helper methods to create patches
/// </summary>
public static class Patch {

    /// <summary>
    /// operation used to set values or replace arrays
    /// </summary>
    public const string Op_Replace = "replace";
        
    /// <summary>
    /// operation used to add an item to an array
    /// </summary>
    public const string Op_Add = "add";
        
    /// <summary>
    /// operation used to remove a value from an array or delete a property value
    /// </summary>
    public const string Op_Remove = "remove";

    /// <summary>
    /// operation used to remove a value from an array or delete a property value
    /// </summary>
    public const string Op_Flag = "flag";

    /// <summary>
    /// operation used to remove a value from an array or delete a property value
    /// </summary>
    public const string Op_Unflag = "unflag";

    /// <summary>
    /// creates a replace patch (set a value)
    /// </summary>
    /// <param name="property">property to set</param>
    /// <param name="value">value to set</param>
    /// <returns>patch operation to send to patch endpoints</returns>
    public static PatchOperation Replace(string property, object value) {
	    return new() {
		    Op = "replace",
		    Path = $"/{property.ToLower()}",
		    Value = value
	    };
    }
        
    /// <summary>
    /// adds items to a collection
    /// </summary>
    /// <param name="property">path to collection</param>
    /// <param name="value">item or collection to add</param>
    /// <returns>patch operation to send to patch endpoints</returns>
    public static PatchOperation Add(string property, object value) {
	    return new() {
		    Op = "add",
		    Path = $"/{property.ToLower()}",
		    Value = value
	    };
    }

    /// <summary>
    /// removes items from a collection
    /// </summary>
    /// <param name="property">path to collection</param>
    /// <param name="value">item or collection to add</param>
    /// <returns>patch operation to send to patch endpoints</returns>
    public static PatchOperation Remove(string property, object value) {
	    return new() {
		    Op = "remove",
		    Path = $"/{property.ToLower()}",
		    Value = value
	    };
    }
    
    /// <summary>
    /// sets a flag in a flag array
    /// </summary>
    /// <param name="property">path to flag property</param>
    /// <param name="value">flag to set (should be an integer or enum)</param>
    /// <returns>patch operation to send to patch endpoints</returns>
    public static PatchOperation Flag(string property, object value) {
	    return new() {
		    Op = Op_Flag,
		    Path = $"/{property.ToLower()}",
		    Value = value
	    };
    }
    
    /// <summary>
    /// clears a flag in a flag array
    /// </summary>
    /// <param name="property">path to flag property</param>
    /// <param name="value">flag to clear (should be an integer or enum)</param>
    /// <returns>patch operation to send to patch endpoints</returns>
    public static PatchOperation Unflag(string property, object value) {
	    return new() {
		    Op = Op_Unflag,
		    Path = $"/{property.ToLower()}",
		    Value = value
	    };
    }
}