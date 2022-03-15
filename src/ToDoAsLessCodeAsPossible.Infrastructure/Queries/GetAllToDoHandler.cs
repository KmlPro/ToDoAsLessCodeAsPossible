using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDos;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Queries;

internal class GetAllToDoHandler : IQueryHandler<GetAllToDo,GetAllToDoResult>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetAllToDoHandler(ISqlQueryExecutor sqlQueryExecutor)
    {
        _sqlQueryExecutor = sqlQueryExecutor;
    }

    public async Task<GetAllToDoResult> HandleAsync(GetAllToDo query, CancellationToken cancellationToken)
    {
        var sql = "select Id, Title, IsCompleted from ToDo";
        var toDos = await _sqlQueryExecutor.QueryAsync<ToDoDto>(sql, null, cancellationToken);

        return new GetAllToDoResult(toDos);
    }
}