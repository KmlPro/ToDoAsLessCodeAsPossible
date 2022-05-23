namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Exceptions;

public class InvalidCommandStructException : Exception
{
    public InvalidCommandStructException(List<string> errorMessages) : base("Command structure is invalid")
    {
        ErrorMessages = errorMessages;
    }

    public List<string> ErrorMessages { get; }
}