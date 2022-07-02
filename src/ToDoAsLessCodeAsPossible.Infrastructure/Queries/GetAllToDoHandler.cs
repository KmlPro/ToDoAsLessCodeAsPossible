using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Queries;

internal class GetAllToDoHandler : IQueryHandler<GetAllToDo, IEnumerable<ToDoDto>>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;
    private readonly IQueryBuilder _queryBuilder;

    public GetAllToDoHandler(ISqlQueryExecutor sqlQueryExecutor, IQueryBuilder queryBuilder)
    {
        _sqlQueryExecutor = sqlQueryExecutor;
        _queryBuilder = queryBuilder;
    }

    public async Task<IEnumerable<ToDoDto>> HandleAsync(GetAllToDo query, CancellationToken cancellationToken)
    {
        _queryBuilder.WithBaseQuery("select Id, Title, IsCompleted from ToDo");
        _queryBuilder.WithFieldMapping("title", "Title");
        _queryBuilder.WithFieldMapping("completed", "IsCompleted");

        if (query.QueryFilter != null)
        {
            _queryBuilder.WithQueryFilter(query.QueryFilter);
        }

        var sql = _queryBuilder.Build();
        
        var toDos = await _sqlQueryExecutor.QueryAsync<ToDoDto>(sql, null, cancellationToken);

        return toDos;
    }
}