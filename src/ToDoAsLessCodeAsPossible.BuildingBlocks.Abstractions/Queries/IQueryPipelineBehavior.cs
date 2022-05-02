namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public delegate Task<TResult> QueryHandlerDelegate<TResult>();

public interface IQueryPipelineBehavior
{
    Task<TResult> HandleAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken,
        QueryHandlerDelegate<TResult> next)
        where TQuery : IQuery<TResult>;
}