using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetAllToDos;

public static class Endpoint
{
    public static void GetAllToDoEndpoint(this WebApplication app)
    {
        app.MapGet("/todo", async (IQueryDispatcher queryDispatcher, string? filters, CancellationToken token) =>
        {
            var queryFilters = FilterParser.Parse(filters);
            var query = new GetAllToDo(queryFilters);
            
            var result = await queryDispatcher.Handle(query,token);
            return Results.Ok(result);
        });
    }
}