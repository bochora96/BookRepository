using MediatR;

namespace TemoDemo.Application.Features.Book.Queries.GetById;

public record GetByIdQuery(Guid Id) : IRequest<GetByIdDto>;
