namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Validation.Exceptions;

public class InvalidParametersException : Exception
{
    public List<string> ErrorMessages { get; }

    public InvalidParametersException(List<string> errorMessages): base("The object data is invalid")
    {
        ErrorMessages = errorMessages;
    }
}