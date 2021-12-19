
namespace ToDo_Minimal_Api_6.Resources.ToDo;

public static class ToDoRequests
{
    public static WebApplication RegisterToDoResource(this WebApplication app)
    {
        app.MapGet("/todo", ToDoRequests.GetAll);
        return app;
    }

    private static IResult GetAll()
    {
        return Results.Ok();
    }
}