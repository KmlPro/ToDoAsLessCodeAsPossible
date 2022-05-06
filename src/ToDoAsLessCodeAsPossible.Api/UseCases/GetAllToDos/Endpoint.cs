using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetAllToDos;

public static class Endpoint
{
    public static void GetAllToDoEndpoint(this WebApplication app)
    {
        app.MapGet("/todo", async (IQueryDispatcher queryDispatcher, CancellationToken token) =>
        {
            var query = new GetAllToDo();
            var result = await queryDispatcher.Handle(query,token);
            return Results.Ok(result);
        });
    }
}