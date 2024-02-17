using System.Net;
using Newtonsoft.Json;
using TemoDemo.Application.Exceptions;
using TemoDemo.Application.Responses;

namespace DemoTemo.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        switch (ex)
        {
            case CustomValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                var errors = validationException.Failures.Select(item => $"{item.Key} : {item.Value[0]}").ToList();

                var validationProblemDetails = BaseResponse.CreateProblemDetails(
                    "Validation error occured",
                    "Validation error occured",
                    "One or more errors occurred",
                    (int)HttpStatusCode.UnprocessableEntity,
                    errors
                );

                var validationBaseResponse = new BaseResponse(validationProblemDetails, false);

                return context.Response.WriteAsync(JsonConvert.SerializeObject(validationBaseResponse));
            case NotFoundException notFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                var notFoundProblemDetails = BaseResponse.CreateProblemDetails(
                    "Not found",
                    "Not found",
                    "One or more errors occurred",
                    (int)HttpStatusCode.NotFound,
                    [ notFoundException.Message ]
                );

                var notFoundBaseResponse = new BaseResponse(notFoundProblemDetails, false);

                return context.Response.WriteAsync(JsonConvert.SerializeObject(notFoundBaseResponse));
            case CannotAddEntityException cannotAddEntityException:

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var cannotAddEntityProblemDetails = BaseResponse.CreateProblemDetails(
                    "Bad Request",
                    "Bad Request",
                    "One or more errors occurred",
                    (int)HttpStatusCode.BadRequest,
                    [ cannotAddEntityException.Message ]
                );

                var cannotAddEntityBaseResponse = new BaseResponse(cannotAddEntityProblemDetails, false);

                return context.Response.WriteAsync(JsonConvert.SerializeObject(cannotAddEntityBaseResponse));
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var defaultProblemDetails = new BaseResponse(
                    BaseResponse.CreateProblemDetails(
                        "Internal Server Error",
                        "Internal Server Error",
                        "One or more errors occurred",
                        StatusCodes.Status500InternalServerError,
                        [ ex.Message ]
                    ),
                    false
                );

                return context.Response.WriteAsync(JsonConvert.SerializeObject(defaultProblemDetails));
        }
    }
}
