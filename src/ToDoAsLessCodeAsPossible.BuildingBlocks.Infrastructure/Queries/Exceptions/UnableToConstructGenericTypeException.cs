namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

public class UnableToConstructGenericTypeException : Exception
{
    public UnableToConstructGenericTypeException(string queryType) : base(
        $"Unable to create types based on query type for '{queryType}'") { }
}