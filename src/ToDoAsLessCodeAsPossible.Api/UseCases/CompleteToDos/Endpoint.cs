using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CompleteToDos;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.CompleteToDos;

public static class Endpoint
{
    public static void CompleteToDoEndpoint(this WebApplication app)
    {
        app.MapPost("/todo/complete", async (CompleteToDoRequest request, ICommandDispatcher commandDispatcher, CancellationToken token) =>
        {
            var command = new CompleteToDo(request.Id);
            await commandDispatcher.SendAsync(command,token);
            return Results.Ok();
        });
    }
}