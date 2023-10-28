using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityFm.Exceptions;

public class GeneralExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var error = new ErrorDetails();

        if (context.Exception is ArgumentNullException)
        {
            error.StatusCode = StatusCodes.Status400BadRequest;
            error.Message = "An unexpected empty data has been found";

            context.Result = new JsonResult(error);
            context.ExceptionHandled = true;
        }

        if (!context.ExceptionHandled)
        {
            error.StatusCode = StatusCodes.Status500InternalServerError;
            error.Message = $"An unexpected error has been found: {context.Exception.Message}";

            context.Result = new JsonResult(error);
            context.ExceptionHandled = true;
        }
    }
}