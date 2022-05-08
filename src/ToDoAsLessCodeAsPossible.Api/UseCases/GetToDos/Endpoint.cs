using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.BuildInExceptions;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetToDos;

public static class Endpoint
{
    public static void GetToDoEndpoint(this WebApplication app)
    {
        app.MapGet("/todo/{id}", async (string id, IQueryDispatcher queryDispatcher, CancellationToken token) =>
        {
            RequestValidator.ValidateAndThrow(id);
            var query = new GetToDo(new Guid(id));
            
            var result = await queryDispatcher.Handle(query,token);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return result == null ? Results.NotFound() : Results.Ok(result);
        });
    }
}