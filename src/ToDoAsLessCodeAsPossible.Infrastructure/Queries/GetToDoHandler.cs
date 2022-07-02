using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Queries;

internal class GetToDoHandler : IQueryHandler<GetToDo,ToDoDto>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;
    public GetToDoHandler(ISqlQueryExecutor sqlQueryExecutor)
    {
        _sqlQueryExecutor = sqlQueryExecutor;
    }

    public async Task<ToDoDto> HandleAsync(GetToDo query, CancellationToken cancellationToken)
    {
        var parameters = new { query.Id };
        var sql = "select Id, Title, IsCompleted from ToDo where Id == @Id";

        var toDos = await _sqlQueryExecutor.QueryFirstOrDefaultAsync<ToDoDto>(sql, parameters, cancellationToken);
        
        return toDos;
    }
}