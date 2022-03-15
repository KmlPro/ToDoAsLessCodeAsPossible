using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDos;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Queries;

internal class GetToDoHandler : IQueryHandler<GetToDo,GetToDoResult>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetToDoHandler(ISqlQueryExecutor sqlQueryExecutor)
    {
        _sqlQueryExecutor = sqlQueryExecutor;
    }

    public async Task<GetToDoResult> HandleAsync(GetToDo query, CancellationToken cancellationToken)
    {
        var parameters = new { Id = query.Id };
        var sql = "select Id, Title, IsCompleted from ToDo where Id == @Id";
        var toDos = await _sqlQueryExecutor.QueryFirstOrDefaultAsync<ToDoDto>(sql, parameters, cancellationToken);
        
        //TO DO Exception not found 
        return new GetToDoResult(toDos);
    }
}