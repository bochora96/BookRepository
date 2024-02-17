using MediatR;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;

namespace TemoDemo.Application.Features.Book.Queries.GetById;

public class GetByIdQueryHandler(IBookRepository bookRepository)
    : IRequestHandler<GetByIdQuery, GetByIdDto>
{
    public async Task<GetByIdDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await bookRepository.GetById(request.Id, cancellationToken);

        if (result is null)
        {
            throw new NotFoundException($"The book with id {request.Id}");
        }

        var dto = new GetByIdDto(result.Id, result.Title, result.Author, result.DateOfPublication);

        return dto;
    }
}
