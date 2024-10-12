using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pooshit.AspNetCore.Services.Errors.Handlers;

/// <summary>
/// handles specific exception types of requests
/// </summary>
public interface IErrorHandler {

    /// <summary>
    /// type of exception to handle
    /// </summary>
    Type ExceptionType { get; }

    /// <summary>
    /// handles a request error
    /// </summary>
    /// <param name="error">error thrown by request</param>
    /// <param name="response">response to which to write information</param>
    /// <param name="responseavailable">determines whether response can be used to write error information</param>
    Task HandleError(Exception error, HttpResponse response, bool responseavailable);
}