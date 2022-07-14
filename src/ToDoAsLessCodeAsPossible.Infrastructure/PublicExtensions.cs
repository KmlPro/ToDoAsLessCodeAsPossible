using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence;
using ToDoAsLessCodeAsPossible.UseCases;

namespace ToDoAsLessCodeAsPossible.Infrastructure;

public static class PublicExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var useCasesAssembly = typeof(UseCaseMarker).Assembly;
        var infrastructureAssembly = typeof(PublicExtensions).Assembly;
        
        services.AddCommands(useCasesAssembly);
        services.AddQueries(infrastructureAssembly);
        services.AddPersistence();

        return services;
    }
}