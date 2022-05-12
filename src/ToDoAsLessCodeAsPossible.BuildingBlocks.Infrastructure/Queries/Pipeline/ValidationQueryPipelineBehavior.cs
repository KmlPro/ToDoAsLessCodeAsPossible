using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Pipeline;

public class ValidationQueryPipelineBehavior: IQueryPipelineBehavior
{
    public async Task<TResult> HandleAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken, QueryHandlerDelegate<TResult> next) where TQuery : IQuery<TResult>
    {
        return await next().ConfigureAwait(false);
    }
}