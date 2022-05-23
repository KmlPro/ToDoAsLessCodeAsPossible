using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries;

internal sealed class QueryDispatcher : IQueryDispatcher
{
    private const string QUERY_HANDLE_METHOD_NAME = "HandleAsync";
    private readonly IServiceScopeFactory _serviceFactory;

    public QueryDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    //Credits to: https://github.com/jbogard/MediatR/blob/8492e9e050e87c4e6e9837bd3af8cd6506aaa4af/src/MediatR/Wrappers/RequestHandlerWrapper.cs for PipelineBehaviour
    //Also credits to: https://codewithshadman.com/cqrs-design-pattern-csharp/#segregating-commands-and-queries for inspiration how to create Dispatcher without need to specify types during execute Handle method
    public async Task<TResult> Handle<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        using var scope = _serviceFactory.CreateScope();
        var provider = scope.ServiceProvider;

        var queryType = query.GetType();
        var queryName = queryType.Name;

        var handlerType = GetHandlerType(queryType, typeof(TResult));

        if (handlerType == null)
        {
            throw new UnableToConstructGenericTypeException(queryName);
        }

        dynamic? handler = provider.GetService(handlerType);

        if (handler == null)
        {
            throw new QueryHandlerNotFoundException(queryName);
        }

        MethodInfo handleAsyncMethod = MethodInfoGetter.GetByName(handler, QUERY_HANDLE_METHOD_NAME);
        if (handleAsyncMethod == null)
        {
            throw new MethodNotFoundInGenericTypeException(queryName, QUERY_HANDLE_METHOD_NAME);
        }

        Task<TResult> Handler() =>
            MethodExecutor.InvokeAsync<TResult>(handleAsyncMethod, handler, (dynamic)query, cancellationToken);

        return await provider
            .GetServices<IQueryPipelineBehavior>()
            .Reverse()
            .Aggregate((QueryHandlerDelegate<TResult>)Handler,
                (next, pipeline) => async () => await pipeline.HandleAsync(query, cancellationToken, next))();
    }

    private Type GetHandlerType(Type queryType, Type resultType)
    {
        var type = typeof(IQueryHandler<,>);

        var queryAndResultTypes = new[] { queryType, resultType };
        var handlerType = type.MakeGenericType(queryAndResultTypes);

        return handlerType;
    }
}