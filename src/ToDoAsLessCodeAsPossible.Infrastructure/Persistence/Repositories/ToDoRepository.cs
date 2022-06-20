using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;
using ToDoAsLessCodeAsPossible.Domain.Entity;
using ToDoAsLessCodeAsPossible.Domain.Repository;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence.Repositories;

internal class ToDoRepository : IToDoRepository
{
    private readonly ToDoWriteDbContext _writeDbContext;
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public ToDoRepository(ToDoWriteDbContext writeDbContext, ISqlQueryExecutor sqlQueryExecutor)
    {
        _writeDbContext = writeDbContext;
        _sqlQueryExecutor = sqlQueryExecutor;
    }

    public async Task AddAsync(ToDo toDoEntity, CancellationToken token)
    {
        await _writeDbContext.ToDo.AddAsync(toDoEntity, token);
    }

    public async Task<bool> Exists(string title, CancellationToken token)
    {
        var parameters = new { title };
        var sql = "select Id from ToDo where Title == @title";
        var toDo = await _sqlQueryExecutor.QueryFirstOrDefaultAsync<string?>(sql, parameters, token);
        var exists = toDo != null;
        
        return exists;
    }
}