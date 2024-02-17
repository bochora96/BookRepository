namespace TemoDemo.Application.Features.Book.Commands.Add;

public record AddDto(
    Guid Id,
    string Title,
    string Author,
    DateTime DateOfPublication
);
