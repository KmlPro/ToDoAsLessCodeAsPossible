using ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;

namespace ToDoAsLessCodeAsPossible.Api;

public static class EndpointsConfiguration
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapPostToCommand<CreateToDoRequest, CreateToDo>("/todo");
    }
}