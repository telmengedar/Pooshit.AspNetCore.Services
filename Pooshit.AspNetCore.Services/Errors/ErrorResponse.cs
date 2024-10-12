namespace Pooshit.AspNetCore.Services.Errors;

/// <summary>
/// response which describes an error of a request
/// </summary>
public class ErrorResponse {

    /// <summary>
    /// creates a new <see cref="ErrorResponse"/>
    /// </summary>
    public ErrorResponse() {
    }

    /// <summary>
    /// creates a new <see cref="ErrorResponse"/>
    /// </summary>
    /// <param name="code">unique error code</param>
    /// <param name="text">readable text describing error</param>
    /// <param name="context">context data for error (optional)</param>
    public ErrorResponse(string code, string text, object context = null) {
        Code = code;
        Text = text;
        Context = context;
    }

    /// <summary>
    /// unique error code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// readable text describing error
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// context data for error
    /// </summary>
    public object Context { get; set; }
}