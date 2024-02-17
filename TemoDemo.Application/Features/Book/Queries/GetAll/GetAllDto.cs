namespace TemoDemo.Application.Features.Book.Queries.GetAll;

public record GetAllDto(List<BookDto> Books);

public record BookDto(
    Guid Id,
    string Title,
    string Author,
    DateTime DateOfPublication
);
