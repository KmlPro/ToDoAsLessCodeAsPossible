using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDos;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;

public static class Endpoint
{
    public static void CreateToDoEndpoint(this WebApplication app)
    {
        app.MapPost("/todo", async (CreateToDoRequest request, ICommandDispatcher commandDispatcher, CancellationToken token) =>
        {
            var command = new CreateToDo(request.Title);
            await commandDispatcher.SendAsync(command,token);
            // kbytner 18.04.2022 - TO DO should be changed to created
            return Results.Ok();
        });
    }
}