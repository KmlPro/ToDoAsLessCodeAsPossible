namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

public class RequestToUseCaseMapHandlerNotFoundException : Exception
{
    public RequestToUseCaseMapHandlerNotFoundException(string requestName, string useCaseName)
        : base($"RequestToUseCaseMapHandler not found for request '{requestName}' and use case '{useCaseName}"){ }
}