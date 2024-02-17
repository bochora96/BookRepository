using System.Collections.Concurrent;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Exceptions;
using TemoDemo.Domain;

namespace TemoDemo.Persistence;

public class BookRepository : IBookRepository
{
    private readonly ConcurrentDictionary<Guid, Book> _books = new();

    public BookRepository()
    {
        var id = Guid.Parse("6E59868E-9930-4856-9567-2263BAFA9947");

        _books.TryAdd(
            id,
            new Book
            {
                Id = id,
                Author = "Temo",
                Title = "Introduction to c#",
                DateOfPublication = DateTime.Now.AddDays(-10)
            }
        );
    }

    public Task<Book> Add(Book book, CancellationToken cancellationToken)
    {
        if (_books.TryAdd(book.Id, book))
        {
            return Task.FromResult(book);
        }

        throw new CannotAddEntityException(book.Id);
    }

    public Task Remove(Guid id, CancellationToken cancellationToken)
        => Task.FromResult(_books.TryRemove(id, out _));

    public Task Update(Book book, CancellationToken cancellationToken)
    {
        _books[book.Id] = book;
        return Task.CompletedTask;
    }

    public Task<Book?> GetById(Guid id, CancellationToken cancellationToken) =>
        Task.FromResult(_books.GetValueOrDefault(id));

    public Task<ICollection<Book>> GetAll(CancellationToken cancellationToken) =>
        Task.FromResult(_books.Values);
}
