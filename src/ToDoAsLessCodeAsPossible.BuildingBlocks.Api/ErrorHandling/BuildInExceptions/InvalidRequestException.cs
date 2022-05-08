namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.BuildInExceptions;

public class InvalidRequestException : Exception
{
    public List<string> ErrorMessages { get; }

    public InvalidRequestException(List<string> errorMessages) : base("Invalid request parameters")
    {
        ErrorMessages = errorMessages;
    }
}