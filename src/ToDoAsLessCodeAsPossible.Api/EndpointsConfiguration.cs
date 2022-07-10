using ToDoAsLessCodeAsPossible.Api.UseCases.ChangeToDoTitles;
using ToDoAsLessCodeAsPossible.Api.UseCases.CompleteToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.GetAllToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.GetToDos;
using ToDoAsLessCodeAsPossible.Api.UseCases.MarkToDoAsUncompleteds;

namespace ToDoAsLessCodeAsPossible.Api;

public static class EndpointsConfiguration
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.CreateToDoEndpoint();
        app.GetToDoEndpoint();
        app.GetAllToDoEndpoint();
        app.CompleteToDoEndpoint();
        app.MarkToDoAsUncompletedEndpoint();
        app.ChangeToDoTitleEndpoint();
    }
}