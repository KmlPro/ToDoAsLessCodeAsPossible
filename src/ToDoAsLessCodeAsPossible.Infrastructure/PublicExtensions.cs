using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence;
using ToDoAsLessCodeAsPossible.UseCases;

namespace ToDoAsLessCodeAsPossible.Infrastructure;

public static class PublicExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(UseCaseMarker).Assembly;
        
        services.AddCommands(assembly);
        services.AddPersistence();

        return services;
    }
}