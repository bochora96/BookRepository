using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Commands.Add;

namespace Demo.Temo.Tests.Unit.Features.Book.Commands.Add;

public class AddCommandHandlerFixture
{
    public readonly IBookRepository BookRepository = Substitute.For<IBookRepository>();
    public readonly AddCommandHandler AddCommandHandler;
    public readonly AddCommandValidator Validator;

    public AddCommandHandlerFixture()
    {
        AddCommandHandler = new AddCommandHandler(BookRepository);
        Validator = new AddCommandValidator();
    }
}
