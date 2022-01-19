using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;
using ToDoAsLessCodeAsPossible.Domain.Repository;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence.Repositories;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

internal static class Extensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IToDoRepository, ToDoRepository>();
        
        services.AddInMemoryDatabase<ToDoWriteDbContext>(new InMemoryDatabaseParameters(InMemoryDatabaseProvider.Sqlite));
       
        return services;
    }
}