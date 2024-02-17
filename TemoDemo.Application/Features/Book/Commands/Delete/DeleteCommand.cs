using MediatR;

namespace TemoDemo.Application.Features.Book.Commands.Delete;

public record DeleteCommand(Guid Id) : IRequest<DeleteDto>;
