namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Payload;

public record ErrorPayload(string Title, List<string> ErrorMessages);