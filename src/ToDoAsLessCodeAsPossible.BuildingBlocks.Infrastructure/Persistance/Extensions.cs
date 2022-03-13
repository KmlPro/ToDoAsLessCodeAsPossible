using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Databases.Sqlite;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory.DatabaseConfiguration;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance;

public static class Extensions
{
    /// <summary>
    /// Setup in memory Database configuration. Also it registers DbContext and ISqlQueryExecutor in container
    /// </summary>
    public static IServiceCollection AddInMemoryDatabase<TDbContext>(this IServiceCollection services,
        InMemoryDatabaseParameters parameters) where TDbContext : DbContext
    {
        var optionsBuilder = parameters.DatabaseProvider switch
        {
            InMemoryDatabaseProvider.Sqlite => new InMemorySqliteConfigurationFactory().Create(),
            InMemoryDatabaseProvider.EfCore => new InMemoryEfDatabaseConfigurationFactory().Create(),
            _ => throw new NotSupportedException(
                $"Database Provider {parameters.DatabaseProvider.ToString()} is not supported")
        };

        services.AddDbContext<TDbContext>(optionsBuilder);
        services.AddScoped<ITransactionScopeFactory>(services =>
            new TransactionScopeFactory(services.GetRequiredService<TDbContext>()));

        //kbytner 13.03.2022 - temporary solution 
        services.AddScoped<ISqlQueryExecutor>(x => new SqlQueryExecutor(InMemorySqliteConfigurationFactory.ConnectionString));

        return services;
    }

    /// <summary>
    /// Sqlite Database configuration injected to DbContext. Also it registers ISqlQueryExecutor in container
    /// </summary>
    public static IServiceCollection AddSqliteDatabase<TDbContext>(this IServiceCollection services,
        SqliteDatabaseParameters parameters) where TDbContext : DbContext
    {
        var optionsBuilder = new SqliteConfigurationFactory().Create(parameters);

        services.AddDbContext<TDbContext>(optionsBuilder);
        services.AddScoped<ITransactionScopeFactory>(services =>
            new TransactionScopeFactory(services.GetRequiredService<TDbContext>()));

        services.AddScoped<ISqlQueryExecutor>(x => new SqlQueryExecutor(parameters.ConnectionString));

        return services;
    }
}