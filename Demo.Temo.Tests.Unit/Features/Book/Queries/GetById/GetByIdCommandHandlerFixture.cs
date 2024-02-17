using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Queries.GetById;

namespace Demo.Temo.Tests.Unit.Features.Book.Queries.GetById;

public class GetByIdQueryHandlerFixture
{
    public readonly IBookRepository BookRepository = Substitute.For<IBookRepository>();
    public readonly GetByIdQueryHandler GetByIdQueryHandler;

    public GetByIdQueryHandlerFixture()
    {
        GetByIdQueryHandler = new GetByIdQueryHandler(BookRepository);
    }
}
