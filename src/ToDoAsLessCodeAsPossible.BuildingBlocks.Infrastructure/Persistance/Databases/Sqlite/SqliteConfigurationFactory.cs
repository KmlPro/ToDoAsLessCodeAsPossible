using EntityFramework.Exceptions.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Databases.Sqlite;

public class SqliteConfigurationFactory
{
    public Action<DbContextOptionsBuilder> Create(SqliteDatabaseParameters parameters)
    {
        return builder =>
        {
            builder.UseSqlite(parameters.ConnectionString);
            builder.UseExceptionProcessor();
        };
    }
}