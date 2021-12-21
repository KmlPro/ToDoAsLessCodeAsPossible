namespace ToDoAsLessCodeAsPossible.Api.Resources.ToDo;

public static class ToDoRequests
{
    public static void RegisterToDoResource(this WebApplication app)
    {
        app.MapGet("/todo", GetAll);
    }

    private static IResult GetAll()
    {
        return Results.Ok();
    }
}