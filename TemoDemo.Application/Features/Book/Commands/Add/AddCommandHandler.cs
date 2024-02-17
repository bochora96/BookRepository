using MediatR;
using TemoDemo.Application.Contracts.Persistence;

namespace TemoDemo.Application.Features.Book.Commands.Add;

public class AddCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<AddCommand, AddDto>
{
    public async Task<AddDto> Handle(AddCommand request, CancellationToken cancellationToken)
    {
        var bookEntity = new Domain.Book
        {
            Id = Guid.NewGuid(),
            Author = request.Author,
            Title = request.Title,
            DateOfPublication = request.DateOfPublication
        };

        var addedBook = await bookRepository.Add(bookEntity, cancellationToken);

        var addDto = new AddDto(
            addedBook.Id,
            addedBook.Title,
            addedBook.Author,
            addedBook.DateOfPublication
        );

        return addDto;
    }
}
