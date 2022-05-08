using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.BuildInExceptions;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetToDos;

public static class RequestValidator
{
    private const string IdNotNullOrEmpty = "id shouldn't be null or empty";
    private const string IdShouldBeGuid = "id should be guid";

    public static void ValidateAndThrow(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new InvalidRequestException(new List<string>() {IdNotNullOrEmpty});
        }

        if (!Guid.TryParse(id, out _))
        {
            throw new InvalidRequestException(new List<string>() {IdShouldBeGuid});
        }
    }
}