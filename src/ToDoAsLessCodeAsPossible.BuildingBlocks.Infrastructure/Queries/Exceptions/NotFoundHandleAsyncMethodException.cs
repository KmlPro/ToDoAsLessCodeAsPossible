namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

public class NotFoundHandleAsyncMethodException: Exception
{
    public NotFoundHandleAsyncMethodException(string queryType) : base(
        $"Query Handler does not contains method HandleAsync. Query type '{queryType}'") { }
}