using ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.GetToDos;

namespace ToDoAsLessCodeAsPossible.Api;

public static class EndpointsConfiguration
{
    //add validation on command/query level
    //remove mapPostToCommand and use only ICommandDispatcher and IQueryDispatcher
    //each mappost/mapquery should be in use cases
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.CreateToDoEndpoint();
        app.GetToDoEndpoint();
        //   app.MapGetToQuery<GetToDoRequest, GetToDo, GetToDoResult>("/todo");
        //   app.MapGetToQuery<GetAllToDo, GetAllToDoResult>("/todo");
    }
}