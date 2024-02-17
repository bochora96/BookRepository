using MediatR;

namespace TemoDemo.Application.Features.Book.Commands.Add;

public record AddCommand(
    string Title,
    string Author,
    DateTime DateOfPublication
) : IRequest<AddDto>;
