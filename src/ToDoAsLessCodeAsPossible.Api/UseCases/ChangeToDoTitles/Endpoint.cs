using ToDoAsLessCodeAsPossible.Api.UseCases.CompleteToDos;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.UseCases.Commands.ChangeToDoTitles;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CompleteToDos;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.ChangeToDoTitles;

public static class Endpoint
{
    public static void ChangeToDoTitleEndpoint(this WebApplication app)
    {
        app.MapPost("/todo/changeTitle", async (ChangeToDoTitleRequest request, ICommandDispatcher commandDispatcher, CancellationToken token) =>
        {
            var command = new ChangeToDoTitle(request.Id, request.Title);
            await commandDispatcher.SendAsync(command,token);
            return Results.Ok();
        });
    }
}