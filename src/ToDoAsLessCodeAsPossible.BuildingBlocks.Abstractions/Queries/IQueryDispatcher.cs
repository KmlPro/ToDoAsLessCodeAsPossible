namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IQueryDispatcher
{
    public Task<TResult> HandleAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : class;
}