using System.Net;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling;

public record ErrorResponse(HttpStatusCode StatusCode, object Response);