namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

public class RequestValidationException : Exception
{
    public List<string> ErrorMessages { get; }

    public RequestValidationException(List<string> errorMessages): base("Request data is invalid")
    {
        ErrorMessages = errorMessages;
    }
}