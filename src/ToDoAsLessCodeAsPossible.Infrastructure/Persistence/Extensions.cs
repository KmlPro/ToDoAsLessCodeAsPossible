using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence.Repositories;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IToDoWriteRepository, ToDoWriteRepository>();
        
        services.AddInMemoryDatabase<ToDoWriteDbContext>(new InMemoryDatabaseParameters(InMemoryDatabaseProvider.Sqlite));
       
        return services;
    }
}