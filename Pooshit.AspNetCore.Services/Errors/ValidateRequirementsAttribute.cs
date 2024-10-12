using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Pooshit.AspNetCore.Services.Errors;

/// <summary>
/// catches results of controller actions and converts results of failed validations to proper <see cref="ErrorResponse"/>s
/// </summary>
public class ValidateRequirementsAttribute : ActionFilterAttribute {
    readonly ILogger logger;

    /// <summary>
    /// creates a new <see cref="ValidateRequirementsAttribute"/>
    /// </summary>
    /// <param name="logger">access to logging</param>
    public ValidateRequirementsAttribute(ILogger logger) {
        this.logger = logger;
    }

    void LogResult(HttpContext context) {
        StringBuilder sb = new();
        sb.AppendLine($"{context.Request.Method} {context.Request.Path} - Bad request");
        sb.AppendLine($"Type: {context.Request.ContentType}");
        sb.AppendLine($"Length: {context.Request.ContentLength}");
        if(context.Request.HasFormContentType) {
            sb.AppendLine("FormData:");
            sb.AppendLine("\tKeys:");
            foreach(KeyValuePair<string, StringValues> formdata in context.Request.Form)
                sb.AppendLine($"\t\t{formdata.Key}: {formdata.Value}");
            sb.AppendLine("\tFiles: ");
            foreach(IFormFile formFile in context.Request.Form.Files) {
                sb.AppendLine($"\t\tField: {formFile.Name}");
                sb.AppendLine($"\t\tFilename: {formFile.FileName}");
                sb.AppendLine($"\t\tType: {formFile.ContentType}");
                sb.AppendLine($"\t\tDispo: {formFile.ContentDisposition}");
                sb.AppendLine($"\t\tSize: {formFile.Length} bytes");
                sb.AppendLine();
            }

            sb.Length -= 2;
        }
        else
            sb.AppendLine("No Form Data");
            
        logger.LogError("{message}", sb.ToString());
    }

    void Append(StringBuilder builder, ModelStateDictionary modelstate) {
        builder.AppendLine("Model:");
        foreach (KeyValuePair<string,ModelStateEntry> entry in modelstate) {
            builder.AppendLine($"{entry.Key}: {entry.Value.RawValue} ({entry.Value.ValidationState})");
        }
    }
        
    string ToMessage(SerializableError error, ModelStateDictionary modelstate) {
        StringBuilder message=new();
        foreach (KeyValuePair<string,object> entry in error) {
            message.AppendLine($"{entry.Key}: {entry.Value}");
        }

        Append(message, modelstate);
        return message.ToString();
    }

    string ToMessage(IDictionary<string, string[]> errors, ModelStateDictionary modelstate) {
        StringBuilder message=new();
        foreach (KeyValuePair<string,string[]> entry in errors) {
            message.AppendLine($"{entry.Key}: {string.Join(",", entry.Value)}");
        }

        Append(message, modelstate);
        return message.ToString();
    }
        
    /// <inheritdoc/>
    public override void OnResultExecuting(ResultExecutingContext context) {

        if(context.Result is BadRequestObjectResult result) {
            const string badformattext = "One of the specified parameters was not in an expected format";
            if (result.Value is SerializableError error) {
                logger.LogWarning("Bad request:\n{message}", ToMessage(error, context.ModelState));
                context.Result = new BadRequestObjectResult(new ErrorResponse(DefaultErrorCodes.BadParameter, badformattext, error));
            }
            else if (result.Value is ValidationProblemDetails details) {
                logger.LogWarning("Bad request:\n{message}", ToMessage(details.Errors, context.ModelState));
                context.Result = new BadRequestObjectResult(new ErrorResponse(DefaultErrorCodes.BadParameter, badformattext, details.Errors));
            }
            else {
                StringBuilder builder=new();
                builder.AppendLine("Bad Request");
                Append(builder, context.ModelState);
                logger.LogWarning("{message}", builder.ToString());
                context.Result = new BadRequestObjectResult(new ErrorResponse(DefaultErrorCodes.BadParameter, badformattext));
            }
        }
    }
}