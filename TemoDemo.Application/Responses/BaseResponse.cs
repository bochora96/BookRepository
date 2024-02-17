using Microsoft.AspNetCore.Mvc;

namespace TemoDemo.Application.Responses;

public record BaseResponse(ProblemDetails ProblemDetails, bool Success)
{
    public BaseResponse() : this(null, true)
    {
    }

    public BaseResponse(ProblemDetails problemDetails) : this(problemDetails, true)
    {
    }

    public static ProblemDetails CreateProblemDetails(
        string title,
        string type,
        string detail,
        int status,
        List<string> errors
    ) =>
        new()
        {
            Title = title,
            Type = type,
            Detail = detail,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
