using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;

public static class Extensions
{
    public static IServiceCollection AddRequestToUseCaseMapping(this IServiceCollection services,
        Assembly assembly)
    {
        services.AddSingleton<IRequestMapper, RequestMapper>();
        
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IRequestToUseCaseMapHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}