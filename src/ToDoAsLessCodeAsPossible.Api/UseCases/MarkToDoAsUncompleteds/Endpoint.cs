using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CompleteToDos;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.MarkToDoAsUncompleteds;

public static class Endpoint
{
    public static void MarkToDoAsUncompletedEndpoint(this WebApplication app)
    {
        app.MapPost("/todo/markAsUncompleted", async (MarkToDoAsUnCompletedRequest request, ICommandDispatcher commandDispatcher, CancellationToken token) =>
        {
            var command = new CompleteToDo(request.Id);
            await commandDispatcher.SendAsync(command,token);
            return Results.Ok();
        });
    }
}