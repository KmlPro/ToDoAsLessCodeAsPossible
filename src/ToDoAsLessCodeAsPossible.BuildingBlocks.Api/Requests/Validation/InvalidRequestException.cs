namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

public class InvalidRequestException : Exception
{
    public List<string> ErrorMessages { get; }

    public InvalidRequestException(List<string> errorMessages): base("The request data is invalid")
    {
        ErrorMessages = errorMessages;
    }
}