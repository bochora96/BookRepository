using Microsoft.Extensions.DependencyInjection;
using TemoDemo.Application.Contracts.Persistence;

namespace TemoDemo.Persistence;

public static class AddPersistenceServices
{
    public static IServiceCollection AddPersistence(this IServiceCollection collection)
    {
        // singleton service for in memory db
        // in other scenarios i'd use scoped service injection
        collection.AddSingleton<IBookRepository, BookRepository>();

        return collection;
    }
}
