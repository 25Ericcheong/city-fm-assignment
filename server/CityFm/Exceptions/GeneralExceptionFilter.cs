using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CityFm.Exceptions;

public class GeneralExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var error = new ErrorDetails();

        switch (context.Exception)
        {
            case ExternalApiException:
                error.StatusCode = StatusCodes.Status500InternalServerError;
                error.Message = $"An internal service error has occurred: {context.Exception.Message}";

                context.Result = new JsonResult(error);
                context.ExceptionHandled = true;
                break;
            case ArgumentException:
                error.StatusCode = StatusCodes.Status400BadRequest;
                error.Message = $"An error has occurred due to a client request: {context.Exception.Message}";

                context.Result = new JsonResult(error);
                context.ExceptionHandled = true;
                break;

            case ArgumentNullException or NullReferenceException:
                error.StatusCode = StatusCodes.Status400BadRequest;
                error.Message = $"An unexpected empty data field has been found: {context.Exception.Message}";

                context.Result = new JsonResult(error);
                context.ExceptionHandled = true;
                break;
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