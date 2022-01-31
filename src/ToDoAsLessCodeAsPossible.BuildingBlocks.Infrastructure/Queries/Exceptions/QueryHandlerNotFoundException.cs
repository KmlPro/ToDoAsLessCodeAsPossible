namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

public class QueryHandlerNotFoundException : Exception
{
    public QueryHandlerNotFoundException(string queryType) : base(
        $"Query Handler not found for query '{queryType}'") { }
}