namespace TemoDemo.Application.Features.Book.Queries.GetById;

public record GetByIdDto(
    Guid Id,
    string Title,
    string Author,
    DateTime DateOfPublication
);
