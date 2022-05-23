namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

public class InvalidQueryStructException : Exception
{
    public InvalidQueryStructException(List<string> errorMessages) : base("Query structure is invalid")
    {
        ErrorMessages = errorMessages;
    }

    public List<string> ErrorMessages { get; }
}