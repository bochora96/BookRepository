using FluentAssertions;
using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;
using TemoDemo.Application.Features.Book.Commands.Delete;
using Xunit;

namespace Demo.Temo.Tests.Unit.Features.Book.Commands.Delete;

public class DeleteCommandHandlerTests(DeleteCommandHandlerFixture fixture)
    : IClassFixture<DeleteCommandHandlerFixture>
{
    private readonly IBookRepository _bookRepository = fixture.BookRepository;
    private readonly DeleteCommandHandler _deleteCommandHandler = fixture.DeleteCommandHandler;

    [Fact]
    public void Handle_WhenIdIsNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var command = new DeleteCommand(Guid.NewGuid());
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((TemoDemo.Domain.Book?)null);

        // Act
        Func<Task> act = async () => await _deleteCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenIdExists_ReturnsEmptyObject()
    {
        // Arrange
        var command = new DeleteCommand(Guid.NewGuid());
        var bookEntity = new TemoDemo.Domain.Book
        {
            Id = command.Id,
            Author = "Author",
            Title = "Title",
            DateOfPublication = DateTime.Now
        };
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(bookEntity);

        // Act
        var result = await _deleteCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(new DeleteDto());
    }

    [Fact]
    public async Task Handle_WhenIdExists_CallsRemove()
    {
        // Arrange
        var command = new DeleteCommand(Guid.NewGuid());
        var bookEntity = new TemoDemo.Domain.Book
        {
            Id = command.Id,
            Author = "Author",
            Title = "Title",
            DateOfPublication = DateTime.Now
        };
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(bookEntity);

        // Act
        var result = await _deleteCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        await _bookRepository.Received(1).Remove(command.Id, Arg.Any<CancellationToken>());
    }
}
