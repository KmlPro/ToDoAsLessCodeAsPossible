using System.Net;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions;

public record ErrorResponse(HttpStatusCode StatusCode, object Response);