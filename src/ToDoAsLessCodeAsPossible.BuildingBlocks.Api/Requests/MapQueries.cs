using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;

public static class MapQueries
{
    /// <summary>
    /// Before use, make sure that you added IQueryDispatcher instance by AddQueries() extension.
    /// Map indicated request to specified Query. Request flow:
    /// <list type="bullet">
    /// <item>
    /// <description>Validate Request with Data Annotations attributes</description>
    /// </item>
    /// <item>
    /// <description>Execute Query</description>
    /// </item>
    /// </list>
    /// <exception cref="QueryHandlerNotFoundException">When query handler (IQueryHandler) is not implemented for specified type</exception>
    /// </summary>
    public static void MapGetToQuery<TRequest, TQuery, TQueryResult>(this WebApplication app, string path)
        where TRequest : class where TQuery : class, IQuery where TQueryResult : IQueryResult
    {
        app.MapGet(path, Get<TRequest, TQuery, TQueryResult>);
    }
    
    /// <summary>
    /// Before use, make sure that you added IQueryDispatcher instance by AddQueries() extension.
    /// IMPORTANT! This is for endpoints without ANY parameters, for example for get all items.
    /// Request flow:
    /// <list type="bullet">
    /// <item>
    /// <description>Execute Query</description>
    /// </item>
    /// </list>
    /// <exception cref="QueryHandlerNotFoundException">When query handler (IQueryHandler) is not implemented for specified type</exception>
    /// </summary>
    public static void MapGetToQuery<TQuery, TQueryResult>(this WebApplication app, string path)
         where TQuery : class, IQuery, new() where TQueryResult : IQueryResult
    {
        app.MapGet(path, Get<TQuery, TQueryResult>);
    }

    private static async Task<TQueryResult> Get<TRequest, TQuery, TQueryResult>(
        [FromServices] IQueryDispatcher queryDispatcher,
        [FromServices] RequestMapper mapper, [FromServices] RequestValidator requestValidator,
        [FromQuery] TRequest request,
        CancellationToken cancellationToken)
        where TRequest : class where TQuery : class, IQuery where TQueryResult : IQueryResult
    {
        requestValidator.ValidateAndThrow(request);

        var query = mapper.Map<TRequest, TQuery>(request, cancellationToken);
        var result = await queryDispatcher.Handle<TQuery, TQueryResult>(query, cancellationToken);

        return result;
    }
    
    private static async Task<TQueryResult> Get<TQuery, TQueryResult>(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken cancellationToken)
        where TQuery : class, IQuery, new() where TQueryResult : IQueryResult
    {
        var result = await queryDispatcher.Handle<TQuery, TQueryResult>(new TQuery(), cancellationToken);

        return result;
    }
}