using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pooshit.AspNetCore.Services.Errors;

namespace Pooshit.AspNetCore.Services.Middleware {
    
    /// <summary>
    /// middleware used to handle errors in api chain
    /// </summary>
    public class ErrorHandlerMiddleware {
        readonly IErrorHandlerCollection errorhandlers;
        readonly RequestDelegate next;

        /// <summary>
        /// creates a new <see cref="ErrorHandlerMiddleware"/>
        /// </summary>
        /// <param name="errorhandlers">service containing error handlers</param>
        /// <param name="next">next request in chain to call</param>
        public ErrorHandlerMiddleware(IErrorHandlerCollection errorhandlers, RequestDelegate next) {
            this.errorhandlers = errorhandlers;
            this.next = next;
        }

        /// <summary>
        /// checks the response code and creates an error if an authentication or permission error was encountered
        /// </summary>
        /// <param name="context">context containing response</param>
        public async Task Invoke(HttpContext context) {
            try {
                await next.Invoke(context);
            }
            catch(Exception e) {
                if(e is AggregateException)
                    e = e.InnerException ?? e;

                if (!context.Response.HasStarted) {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,PATCH,DELETE,OPTIONS");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "authorization,content-type");
                    context.Response.Headers.Add("Access-Control-Max-Age", "30");
                    
                }

                await errorhandlers.HandleError(e, context.Response, !context.Response.HasStarted);
            }
        }
    }
}