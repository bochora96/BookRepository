using MediatR;

namespace TemoDemo.Application.Features.Book.Queries.GetAll;

public record GetAllQuery : IRequest<GetAllDto>;
