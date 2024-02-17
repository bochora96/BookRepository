using NSubstitute;
using TemoDemo.Application.Contracts.Persistence;
using TemoDemo.Application.Features.Book.Queries.GetAll;

namespace Demo.Temo.Tests.Unit.Features.Book.Queries.GetAll;

public class GetAllQueryHandlerFixture
{
    public readonly IBookRepository BookRepository = Substitute.For<IBookRepository>();
    public readonly GetAllQueryHandler GetAllQueryHandler;

    public GetAllQueryHandlerFixture()
    {
        GetAllQueryHandler = new GetAllQueryHandler(BookRepository);
    }
}
