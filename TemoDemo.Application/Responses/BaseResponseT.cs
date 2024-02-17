namespace TemoDemo.Application.Responses;

public record BaseResponse<T> : BaseResponse
{
    public T? Response { get; init; }
}
