using MediatR;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;

namespace TemoDemo.Application.Features.Book.Commands.Delete;

public class DeleteCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<DeleteCommand, DeleteDto>
{
    public async Task<DeleteDto> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var result = await bookRepository.GetById(request.Id, cancellationToken);

        if (result is null)
        {
            throw new NotFoundException($"The book with id {request.Id}");
        }

        await bookRepository.Remove(request.Id, cancellationToken);

        return new DeleteDto();
    }
}
