using ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDo;
using ToDoAsLessCodeAsPossible.Api.UseCases.GetToDo;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDos;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDos;

namespace ToDoAsLessCodeAsPossible.Api;

public static class EndpointsConfiguration
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapPostToCommand<CreateToDoRequest, CreateToDo>("/todo");
        app.MapGetToQuery<GetAllToDo, GetAllToDoResult>("/todo");
       // app.MapGetToQuery<GetToDoRequest, GetToDo, GetToDoResult>("/todo"); need to fix that
    }
}