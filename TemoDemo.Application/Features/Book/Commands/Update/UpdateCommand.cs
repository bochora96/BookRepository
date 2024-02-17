using MediatR;

namespace TemoDemo.Application.Features.Book.Commands.Update;

public record UpdateCommand(
    Guid Id,
    string Title,
    string Author,
    DateTime DateOfPublication
) : IRequest<UpdateDto>;

public record UpdateRequest(
    string Title,
    string Author,
    DateTime DateOfPublication
);
