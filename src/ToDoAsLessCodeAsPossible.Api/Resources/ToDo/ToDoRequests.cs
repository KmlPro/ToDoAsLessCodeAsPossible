using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;

namespace ToDoAsLessCodeAsPossible.Api.Resources.ToDo;

public static class ToDoRequests
{
    public record CreateToDoRequest(string Title);

    public static void RegisterToDoResource(this WebApplication app)
    {
        app.MapPostToCommand<CreateToDoRequest, CreateToDo>("/todo");
    }
}