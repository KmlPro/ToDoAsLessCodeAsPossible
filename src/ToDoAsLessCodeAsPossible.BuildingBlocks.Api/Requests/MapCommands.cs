using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;

public static class MapCommands
{
    /// <summary>
    /// Before use, make sure that you added ICommandDispatcher instance by AddCommands() extension.
    /// Map indicated request to specified Command. Request flow:
    /// <list type="bullet">
    /// <item>
    /// <description>Validate Request with Data Annotations attributes</description>
    /// </item>
    /// <item>
    /// <description>Execute Command</description>
    /// </item>
    /// </list>
    /// <exception cref="CommandHandlerNotFoundException">When command handler (ICommandHandler) is not implemented for specified type</exception>
    /// </summary>
    public static void MapPostToCommand<TRequest, TCommand>(this WebApplication app, string path)
        where TRequest : class where TCommand : class, ICommand
    {
        app.MapPost(path, Post<TRequest, TCommand>);
    }

    private static async Task<IResult> Post<TRequest, TCommand>([FromServices] ICommandDispatcher commandDispatcher,
        [FromServices] RequestMapper mapper, [FromServices] RequestValidator requestValidator, TRequest request,
        CancellationToken cancellationToken)
        where TRequest : class where TCommand : class, ICommand
    {
        requestValidator.ValidateAndThrow(request);

        var command = mapper.Map<TRequest, TCommand>(request, cancellationToken);
        await commandDispatcher.SendAsync(command, cancellationToken);

        return Results.Ok();
    }
}