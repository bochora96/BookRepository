using FluentAssertions;
using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Queries.GetAll;
using Xunit;

namespace Demo.Temo.Tests.Unit.Features.Book.Queries.GetAll;

public class GetAllCommandHandlerTests(GetAllQueryHandlerFixture fixture)
    : IClassFixture<GetAllQueryHandlerFixture>
{
    private readonly IBookRepository _bookRepository = fixture.BookRepository;
    private readonly GetAllQueryHandler _getByIdQueryHandler = fixture.GetAllQueryHandler;

    // should return list of elements when list exists
    [Fact]
    public async Task Handle_WhenListExists_ReturnsListOfElements()
    {
        // Arrange
        var query = new GetAllQuery();
        var bookEntities = new List<TemoDemo.Domain.Book>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Author = "Author",
                Title = "Title",
                DateOfPublication = DateTime.Now
            },
            new()
            {
                Id = Guid.NewGuid(),
                Author = "Author",
                Title = "Title",
                DateOfPublication = DateTime.Now
            }
        };
        _bookRepository.GetAll(Arg.Any<CancellationToken>()).Returns(bookEntities);

        var bookEntitiesDto =
            bookEntities.Select(next => new BookDto(next.Id, next.Title, next.Author, next.DateOfPublication)).ToList();

        // Act
        var result = await _getByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new GetAllDto(bookEntitiesDto));
    }

    // should return empty list when list does not exist
    [Fact]
    public async Task Handle_WhenListDoesNotExist_ReturnsEmptyList()
    {
        // Arrange
        var query = new GetAllQuery();
        _bookRepository.GetAll(Arg.Any<CancellationToken>()).Returns(new List<TemoDemo.Domain.Book>());

        // Act
        var result = await _getByIdQueryHandler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(new GetAllDto([]));
    }
}
