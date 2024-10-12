namespace Pooshit.AspNetCore.Services.Errors;

/// <summary>
/// default error codes in mamgo
/// </summary>
public static class DefaultErrorCodes {

    /// <summary>
    /// a required parameter was not specified or a specified parameter was in an unexpected format
    /// </summary>
    public const string BadParameter = "badparameter";

    /// <summary>
    /// a required parameter was not specified or a specified parameter was in an unexpected format
    /// </summary>
    public const string Unhandled = "unhandled";

    /// <summary>
    /// a resource was requested which is protected by a scope not available in the current token
    /// </summary>
    public const string MissingScope = "authorization_missingscope";

    /// <summary>
    /// an invalid token (or no token at all) was found while serving an authorized resource
    /// </summary>
    public const string InvalidToken = "authorization_invalidtoken";

    /// <summary>
    /// error was returned by a http call
    /// </summary>
    public const string HttpErrorStatus = "http_errorstatus";

    /// <summary>
    /// a property was requested which does not exist
    /// </summary>
    public const string DataPropertyNotFound = "data_propertynotfound";
        
    /// <summary>
    /// a requested entity was not found
    /// </summary>
    public const string DataEntityNotFound = "data_entitynotfound";
    
    /// <summary>
    /// out of memory
    /// </summary>
    public const string OOM = "system_oom";
}