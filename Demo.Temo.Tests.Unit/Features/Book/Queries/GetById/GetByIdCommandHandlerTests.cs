using FluentAssertions;
using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;
using TemoDemo.Application.Features.Book.Queries.GetById;
using Xunit;

namespace Demo.Temo.Tests.Unit.Features.Book.Queries.GetById;

public class GetByIdCommandHandlerTests(GetByIdQueryHandlerFixture fixture)
    : IClassFixture<GetByIdQueryHandlerFixture>
{
    private readonly IBookRepository _bookRepository = fixture.BookRepository;
    private readonly GetByIdQueryHandler _getByIdQueryHandler = fixture.GetByIdQueryHandler;


    // check if id not exists return is exception
    [Fact]
    public async Task GetById_WhenIdNotExists_ShouldReturnNotFoundException()
    {
        // Arrange
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((TemoDemo.Domain.Book?)null);

        // Act
        Func<Task> act = async () => await _getByIdQueryHandler.Handle(new GetByIdQuery(Arg.Any<Guid>()), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    // check if id exists return is book
    [Fact]
    public async Task GetById_WhenIdExists_ShouldReturnBook()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookEntity = new TemoDemo.Domain.Book
        {
            Id = id,
            Author = "Author",
            Title = "Title",
            DateOfPublication = DateTime.Now
        };
        _bookRepository.GetById(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(bookEntity);

        var bookDto = new GetByIdDto(id, bookEntity.Title, bookEntity.Author, bookEntity.DateOfPublication);

        // Act
        var result = await _getByIdQueryHandler.Handle(new GetByIdQuery(id), CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(bookDto);
    }
}
