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
    //Also credits to: https://codewithshadman.com/cqrs-design-pattern-csharp/#segregating-commands-and-queries
    public async Task<TResult> Handle<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        using var scope = _serviceFactory.CreateScope();
        var provider = scope.ServiceProvider;
        
        var type = typeof(IQueryHandler<,>);
        var queryType = query.GetType();
        
        var queryAndResultTypes = new[] { queryType, typeof(TResult) };
        var handlerType = type.MakeGenericType(queryAndResultTypes);

        if (handlerType == null)
        {
            throw new UnableToConstructGenericTypeException(queryType.Name);
        }
        
        dynamic handler = provider.GetService(handlerType);
        var handlerTypeActual = handler.GetType();
        var methodsList = handler.GetType().GetMethods();

        if (handler == null)
        {
            throw new QueryHandlerNotFoundException(queryType.Name);
        }

        //find out why there is a problem with find this method
        var result = (TResult)handler.HandleAsync(query, cancellationToken);
        
        Task<TResult> Handler() => handler.HandleAsync(query, cancellationToken);

        return await provider
            .GetServices<IQueryPipelineBehavior>()
            .Reverse()
            .Aggregate((QueryHandlerDelegate<TResult>) Handler, 
                (next, pipeline) => async () => await pipeline.HandleAsync(query, cancellationToken, next))();
    }
}