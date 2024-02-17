using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TemoDemo.Application.Behaviours;

namespace TemoDemo.Application;

public static class AddApplicationServices
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        collection.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        collection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return collection;
    }
}
