using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries;

internal sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public QueryDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    //Credits to: https://github.com/jbogard/MediatR/blob/8492e9e050e87c4e6e9837bd3af8cd6506aaa4af/src/MediatR/Wrappers/RequestHandlerWrapper.cs
    public async Task<TResult> Handle<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : IQuery where TResult : IQueryResult
    {
        using var scope = _serviceFactory.CreateScope();
        var provider = scope.ServiceProvider;
        
        var handler = provider.GetService<IQueryHandler<TQuery,TResult>>();
        if (handler == null)
        {
            throw new QueryHandlerNotFoundException(typeof(TQuery).Name);
        }

        Task<TResult> Handler() => handler.HandleAsync(query, cancellationToken);

        return await provider
            .GetServices<IQueryPipelineBehavior>()
            .Reverse()
            .Aggregate((QueryHandlerDelegate<TResult>) Handler, 
                (next, pipeline) => async () => await pipeline.HandleAsync(query, cancellationToken, next))();
    }
}