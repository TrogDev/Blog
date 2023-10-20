namespace Blog.Web.Middlewares;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using FluentValidation;

public class ValidationExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            ModelStateDictionary modelState = parseErrors(e);
            var response = new BadRequestObjectResult(modelState);
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }

    private ModelStateDictionary parseErrors(ValidationException exception)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in exception.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return modelState;
    }
}