using System.Data.Common;
using EntityFramework.Exceptions.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory.DatabaseConfiguration;

public class InMemorySqliteConfigurationFactory
{
    private DbConnection _dbConnection;

    public InMemorySqliteConfigurationFactory()
    {
        _dbConnection = CreateInMemoryDatabaseConnection();
    }

    public Action<DbContextOptionsBuilder> Create()
    {
        return options =>
        {
            options.UseSqlite(_dbConnection);
            options.UseExceptionProcessor();
            options.EnableSensitiveDataLogging();
            options.ConfigureWarnings(x =>x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning));
        };
    }

    private static DbConnection CreateInMemoryDatabaseConnection()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.OpenAsync().GetAwaiter().GetResult();
        return connection;
    }
}