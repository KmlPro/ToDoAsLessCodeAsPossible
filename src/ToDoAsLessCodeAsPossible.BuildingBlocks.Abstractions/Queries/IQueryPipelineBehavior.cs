namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public delegate Task<TResult> QueryHandlerDelegate<TResult>() where TResult: IQueryResult;

public interface IQueryPipelineBehavior
{
    Task<TResult> HandleAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken,
        QueryHandlerDelegate<TResult> next)
        where TQuery : IQuery where TResult : IQueryResult;
}