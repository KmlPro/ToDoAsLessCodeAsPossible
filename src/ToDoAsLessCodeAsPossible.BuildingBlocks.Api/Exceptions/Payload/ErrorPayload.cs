namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Payload;

public record ErrorPayload(string Title, List<string> ErrorMessages);