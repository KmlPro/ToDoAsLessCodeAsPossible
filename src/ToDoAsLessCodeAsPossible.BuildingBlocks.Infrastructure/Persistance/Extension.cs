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
        Action<DbContextOptionsBuilder> optionsBuilder;

        optionsBuilder = parameters.DatabaseProvider switch
        {
            InMemoryDatabaseProvider.Sqlite => new InMemorySqliteConfigurationFactory().Create(),
            InMemoryDatabaseProvider.EfCore => new InMemoryEfDatabaseConfigurationFactory().Create(),
            _ => throw new NotSupportedException(
                $"Database Provider {parameters.DatabaseProvider.ToString()} is not supported")
        };

        //i need to think about how to add StronglyTypedIdValueConverterSelector to DbContext. And next step will be add IUnitOfWork Interface to Pipeline 
        //https://github.com/KmlPro/IncidentManagmentSystem/blob/e170d6ea67f8b6513c04197d49c1daff867c3493/src/IncidentReport.Infrastructure/Persistence/Configurations/PersistanceModule.cs
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