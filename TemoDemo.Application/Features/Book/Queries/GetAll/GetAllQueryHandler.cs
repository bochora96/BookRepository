using MediatR;
using TemoDemo.Application.Contracts.Persistence;

namespace TemoDemo.Application.Features.Book.Queries.GetAll;

public class GetAllQueryHandler(IBookRepository bookRepository)
    : IRequestHandler<GetAllQuery, GetAllDto>
{
    public async Task<GetAllDto> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var result = await bookRepository.GetAll(cancellationToken);

        var books = result.Select(element =>
            new BookDto(
                element.Id,
                element.Title,
                element.Author,
                element.DateOfPublication
            )
        ).ToList();

        return new GetAllDto(books);
    }
}
