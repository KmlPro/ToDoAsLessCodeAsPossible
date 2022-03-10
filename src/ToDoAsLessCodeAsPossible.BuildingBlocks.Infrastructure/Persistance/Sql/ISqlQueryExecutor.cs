namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;

public interface ISqlQueryExecutor
{
    public Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);
}