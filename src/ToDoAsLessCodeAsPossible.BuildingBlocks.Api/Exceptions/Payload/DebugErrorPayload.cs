namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Payload;

public record DebugErrorPayload(string Title, string message, string? innerException, string? stackTrace);