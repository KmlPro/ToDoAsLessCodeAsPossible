namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> Handle<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
        where TQuery : IQuery where TResult : IQueryResult;
}