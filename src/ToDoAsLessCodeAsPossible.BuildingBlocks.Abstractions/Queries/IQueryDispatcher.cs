namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IQueryDispatcher
{
    public Task<TResult> Handle<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}