using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pooshit.AspNetCore.Services.Errors;

/// <summary>
/// collection of error handlers
/// </summary>
public interface IErrorHandlerCollection {
        
    /// <summary>
    /// handles an error for a response
    /// </summary>
    /// <param name="error">error to handle</param>
    /// <param name="response">response to write result to</param>
    /// <param name="responseavailable">determines whether an error response can be written</param>
    Task HandleError(Exception error, HttpResponse response, bool responseavailable);
}