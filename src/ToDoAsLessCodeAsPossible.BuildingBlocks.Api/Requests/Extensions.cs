using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;

public static class Extensions
{
    public static IServiceCollection AddRequestToUseCaseMapping(this IServiceCollection services,
        Assembly assembly)
    {
        services.AddSingleton<RequestMapper>();
        services.AddSingleton<RequestValidator>();
        
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestToUseCaseMapHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}