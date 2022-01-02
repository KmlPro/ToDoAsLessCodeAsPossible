namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

internal class RequestToUseCaseMapHandlerNotFoundException : Exception
{
    internal RequestToUseCaseMapHandlerNotFoundException(string requestName, string useCaseName)
        : base($"RequestToUseCaseMapHandler not found for request '{requestName}' and use case '{useCaseName}"){ }
}