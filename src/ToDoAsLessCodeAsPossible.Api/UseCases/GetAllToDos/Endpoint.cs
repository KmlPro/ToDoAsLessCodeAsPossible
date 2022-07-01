using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetAllToDos;

public static class Endpoint
{
    private static readonly FilterDefinition FilterDefinition = new FilterDefinition(new List<FilterDefinitionField>()
    {
        new("title", new List<string>() { "eq" })
    });
    
    public static void GetAllToDoEndpoint(this WebApplication app)
    {
        app.MapGet("/todo", async (IQueryDispatcher queryDispatcher, string? filters, CancellationToken token) =>
        {
            var queryFilters = FilterParser.Parse(filters);
            FilterValidator.ValidateAndThrow(FilterDefinition, queryFilters);
            
            var query = new GetAllToDo(queryFilters);
            
            var result = await queryDispatcher.Handle(query,token);
            return Results.Ok(result);
        });
    }
}