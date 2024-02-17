using FluentAssertions;
using FluentValidation;
using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;
using TemoDemo.Application.Features.Book.Commands.Update;
using Xunit;

namespace Demo.Temo.Tests.Unit.Features.Book.Commands.Update;

public class UpdateCommandHandlerTests(
    UpdateCommandHandlerFixture fixture
) : IClassFixture<UpdateCommandHandlerFixture>
{
    private readonly IBookRepository _bookRepository = fixture.BookRepository;
    private readonly UpdateCommandHandler _addCommandHandler = fixture.UpdateCommandHandler;
    private readonly UpdateCommandValidator _updateCommandValidator = fixture.Validator;

    [Fact]
    public void Handle_WhenCommandIsInvalid_ThrowsValidationException()
    {
        // Arrange
        var command = new UpdateCommand(Guid.Empty, string.Empty, string.Empty, DateTime.MaxValue);

        // Act
        Func<Task> act = async () => await _addCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public void Handle_WhenIdIsNotFound_ThrowsNotFoundException()
    {
        // Arrange
        var command = new UpdateCommand(Guid.NewGuid(), "Title", "Author", DateTime.Now);
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((TemoDemo.Domain.Book?)null);

        // Act
        Func<Task> act = async () => await _addCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public void Handle_WhenCommandIsInvalid_ReturnsValidationErrors()
    {
        // Arrange
        var command = new UpdateCommand(Guid.Empty, string.Empty, string.Empty, DateTime.MaxValue);

        // Act
        Func<Task> act = async () => await _addCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ValidationException>().Where(e => e.Errors.Count() == 3);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ReturnsUpdateDto()
    {
        // Arrange
        var command = new UpdateCommand(Guid.NewGuid(), "Title", "Author", DateTime.Now);
        var bookEntity = new TemoDemo.Domain.Book
        {
            Id = command.Id,
            Author = command.Author,
            Title = command.Title,
            DateOfPublication = command.DateOfPublication
        };
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(bookEntity);
        _bookRepository
            .Update(Arg.Any<TemoDemo.Domain.Book>(), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _addCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<UpdateDto>();
    }
}
