namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions;

public record InvalidRequestParametersPayload(List<string> Messages);