using MediatR;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;

namespace TemoDemo.Application.Features.Book.Commands.Update;

public class UpdateCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<UpdateCommand, UpdateDto>
{
    public async Task<UpdateDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var result = await bookRepository.GetById(request.Id, cancellationToken);

        if (result is null)
        {
            throw new NotFoundException($"The book with id {request.Id}");
        }

        var bookEntity = new Domain.Book
        {
            Id = request.Id,
            Author = request.Author,
            Title = request.Title,
            DateOfPublication = request.DateOfPublication
        };

        await bookRepository.Update(bookEntity, cancellationToken);

        return new UpdateDto();
    }
}
