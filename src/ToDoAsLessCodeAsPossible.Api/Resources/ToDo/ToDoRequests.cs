using Microsoft.AspNetCore.Mvc;
using ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Commands;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;

namespace ToDoAsLessCodeAsPossible.Api.Resources.ToDo;

public static class ToDoRequests
{
    public static void RegisterToDoResource(this WebApplication app)
    {
        app.MapGet("/todo", GetAll);
        app.MapPost("/todo", Post);
    }

    private static IResult GetAll()
    {
        return Results.Ok();
    }

    private record CreateToDoRequest(string Title);
    
    private static async Task<IResult> Post([FromServices] ICommandDispatcher commandDispatcher,[FromBody] CreateToDoRequest request, CancellationToken token)
    {
        var command = new CreateToDo(request.Title);
        await commandDispatcher.SendAsync(command, token);
        
        return Results.Ok();
    }
}