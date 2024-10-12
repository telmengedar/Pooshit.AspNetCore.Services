using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pooshit.AspNetCore.Services.Validation;

/// <summary>
/// empty implementation of an object validator
/// </summary>
public class NoValidator : IObjectModelValidator {

    /// <inheritdoc />
    public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model) {
    }
}