namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Payload;

public record DebugErrorPayload(string Title, string message, string? innerException, string? stackTrace);