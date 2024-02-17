using FluentAssertions;
using FluentValidation;
using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Commands.Add;
using Xunit;

namespace Demo.Temo.Tests.Unit.Features.Book.Commands.Add;

public class AddCommandHandlerTests(
    AddCommandHandlerFixture fixture
) : IClassFixture<AddCommandHandlerFixture>
{
    private readonly IBookRepository _bookRepository = fixture.BookRepository;
    private readonly AddCommandHandler _addCommandHandler = fixture.AddCommandHandler;

    [Fact]
    public void Handle_WhenCommandIsInvalid_ThrowsValidationException()
    {
        // Arrange
        var command = new AddCommand(string.Empty, string.Empty, DateTime.MaxValue);

        // Act
        Func<Task> act = async () => await _addCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ReturnsAddDto()
    {
        // Arrange
        var command = new AddCommand("Title", "Author", DateTime.Now);
        var bookEntity = new TemoDemo.Domain.Book
        {
            Id = Guid.NewGuid(),
            Author = command.Author,
            Title = command.Title,
            DateOfPublication = command.DateOfPublication
        };
        _bookRepository.Add(Arg.Any<TemoDemo.Domain.Book>(), Arg.Any<CancellationToken>()).Returns(bookEntity);

        // Act
        var result = await _addCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<AddDto>();
        result.Id.Should().Be(bookEntity.Id);
        result.Title.Should().Be(bookEntity.Title);
        result.Author.Should().Be(bookEntity.Author);
        result.DateOfPublication.Should().Be(bookEntity.DateOfPublication);
    }
}
