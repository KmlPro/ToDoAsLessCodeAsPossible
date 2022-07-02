using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;

internal class SqlQueryExecutor : ISqlQueryExecutor, IDisposable
{
    private readonly IDbConnection _connection;

    public SqlQueryExecutor(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
    }
    
    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
    {
        return (await _connection.QueryAsync<T>(sql, param)).AsList();
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QuerySingleAsync<T>(sql, param);
    }
    
    public void Dispose()
    {
        _connection.Dispose();
    }
}