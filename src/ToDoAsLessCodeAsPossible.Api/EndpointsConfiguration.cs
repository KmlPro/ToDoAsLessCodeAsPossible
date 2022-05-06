using ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.GetAllToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.GetToDos;

namespace ToDoAsLessCodeAsPossible.Api;

public static class EndpointsConfiguration
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.CreateToDoEndpoint();
        app.GetToDoEndpoint();
        app.GetAllToDoEndpoint();
    }
}