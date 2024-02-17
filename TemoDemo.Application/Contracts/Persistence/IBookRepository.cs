using TemoDemo.Domain;

namespace TemoDemo.Application.Contracts.Persistence;

public interface IBookRepository
{
    public Task<Book> Add(Book book, CancellationToken cancellationToken);

    public Task Remove(Guid id, CancellationToken cancellationToken);

    public Task Update(Book book, CancellationToken cancellationToken);

    public Task<Book?> GetById(Guid id, CancellationToken cancellationToken);

    public Task<ICollection<Book>> GetAll(CancellationToken cancellationToken);
}
