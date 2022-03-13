using System.Data.Common;
using EntityFramework.Exceptions.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.StonglyTypedIdConfiguration;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory.DatabaseConfiguration;

public class InMemorySqliteConfigurationFactory
{
    public const string ConnectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
    
    private DbConnection _dbConnection;

    public InMemorySqliteConfigurationFactory()
    {
        _dbConnection = CreateInMemoryDatabaseConnection();
    }

    public Action<DbContextOptionsBuilder> Create()
    {
        return builder =>
        {
            builder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
            builder.UseSqlite(_dbConnection);
            builder.UseExceptionProcessor();
            builder.EnableSensitiveDataLogging();
            builder.ConfigureWarnings(x =>x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning));
        };
    }

    private static DbConnection CreateInMemoryDatabaseConnection()
    {
        var connection = new SqliteConnection(ConnectionString);
        connection.OpenAsync().GetAwaiter().GetResult();
        return connection;
    }
}