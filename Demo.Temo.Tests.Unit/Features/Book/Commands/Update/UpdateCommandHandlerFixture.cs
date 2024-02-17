using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Commands.Update;

namespace Demo.Temo.Tests.Unit.Features.Book.Commands.Update;

public class UpdateCommandHandlerFixture
{
    public readonly IBookRepository BookRepository = Substitute.For<IBookRepository>();
    public readonly UpdateCommandHandler UpdateCommandHandler;
    public readonly UpdateCommandValidator Validator;

    public UpdateCommandHandlerFixture()
    {
        UpdateCommandHandler = new UpdateCommandHandler(BookRepository);
        Validator = new UpdateCommandValidator();
    }
}
