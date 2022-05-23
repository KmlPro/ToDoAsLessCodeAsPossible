using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetToDos;

public static class Endpoint
{
    public static void GetToDoEndpoint(this WebApplication app)
    {
        app.MapGet("/todo/{id:guid}", async (Guid id, IQueryDispatcher queryDispatcher, CancellationToken token) =>
        {
            var query = new GetToDo(id);
            
            var result = await queryDispatcher.Handle(query,token);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return result == null ? Results.NotFound() : Results.Ok(result);
        });
    }
}