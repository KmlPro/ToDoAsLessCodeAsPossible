using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public delegate Task QueryHandlerDelegate();

public interface IQueryPipelineBehavior
{
    Task<TResult> Handle<TQuery, TResult>(TQuery query, CancellationToken cancellationToken,
        QueryHandlerDelegate next)
        where TQuery : IQuery where TResult : IQueryResult;
}