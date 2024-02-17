using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Commands.Delete;

namespace Demo.Temo.Tests.Unit.Features.Book.Commands.Delete;

public class DeleteCommandHandlerFixture
{
    public readonly IBookRepository BookRepository = Substitute.For<IBookRepository>();
    public readonly DeleteCommandHandler DeleteCommandHandler;

    public DeleteCommandHandlerFixture()
    {
        DeleteCommandHandler = new DeleteCommandHandler(BookRepository);
    }
}
