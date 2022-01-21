using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Databases.Sqlite;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory.DatabaseConfiguration;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance;

public static class Extension
{
    /// <summary>
    /// Setup in memory Database configuration. Also it registers DbContext in container
    /// </summary>
    public static IServiceCollection AddInMemoryDatabase<TDbContext>(this IServiceCollection services,
        InMemoryDatabaseParameters parameters) where TDbContext:DbContext
    {
        var optionsBuilder = parameters.DatabaseProvider switch
        {
            InMemoryDatabaseProvider.Sqlite => new InMemorySqliteConfigurationFactory().Create(),
            InMemoryDatabaseProvider.EfCore => new InMemoryEfDatabaseConfigurationFactory().Create(),
            _ => throw new NotSupportedException(
                $"Database Provider {parameters.DatabaseProvider.ToString()} is not supported")
        };

        services.AddDbContext<TDbContext>(optionsBuilder);

        return services;
    }
    
    /// <summary>
    /// Sqlite Database configuration injected to DbContext
    /// </summary>
    public static IServiceCollection AddSqliteDatabase<TDbContext>(this IServiceCollection services,
        SqliteDatabaseParameters parameters) where TDbContext:DbContext
    {
        var optionsBuilder = new SqliteConfigurationFactory().Create(parameters);

        services.AddDbContext<TDbContext>(optionsBuilder);

        return services;
    }
}