using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries;

public static class Extensions
{
    /// <summary>
    /// Add required instances for Queries Handling
    /// </summary>
    public static IServiceCollection AddQueries(this IServiceCollection services,
        Assembly assembly)
    {
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes => 
                classes.AssignableTo(typeof(IQueryHandler<,>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}