using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhysicsWars.WebApi.Filters;

public class HttpGlobalExceptionFilter : IExceptionFilter
{
    private class ErrorResponse
    {
        public string[]? ValidationErrors { get; init; }
    }

    public void OnException(ExceptionContext context)
    {
        // if exception is ValidationException response with 400 and
        // return validation errors
        if (context.Exception is ValidationException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ErrorResponse
            {
                ValidationErrors = (context.Exception as ValidationException)?.Message.Split("\n")
            };

            context.Result = new JsonResult(response);
        }
    }
}