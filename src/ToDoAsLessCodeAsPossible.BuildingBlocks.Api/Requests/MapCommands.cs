using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
using ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;

public static class MapCommands
{
    public static void MapPostToCommand<TRequest, TCommand>(this WebApplication app, string path) where TRequest : class where TCommand : class, ICommand
    {
        app.MapPost(path, Post<TRequest, TCommand>);
    }

    private static async Task<IResult> Post<TRequest, TCommand>([FromServices] ICommandDispatcher commandDispatcher,
        IRequestMapper mapper, TRequest request, CancellationToken cancellationToken)
        where TRequest : class where TCommand : class, ICommand
    {
        var command = mapper.Map<TRequest, TCommand>(request, cancellationToken);
        await commandDispatcher.SendAsync(command, cancellationToken);

        return Results.Ok();
    }
}