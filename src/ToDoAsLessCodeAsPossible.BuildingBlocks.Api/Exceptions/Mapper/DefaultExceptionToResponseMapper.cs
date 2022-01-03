using System.Net;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Payload;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Mapper;

internal class DefaultExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ErrorResponse? Map<TExceptionType>(TExceptionType exception) where TExceptionType : Exception
        => exception switch
        {
            InvalidRequestException ex => new ErrorResponse(HttpStatusCode.BadRequest,CreatePayload(ex.Message, ex.ErrorMessages)),
            _ => null
        };

    private ErrorPayload CreatePayload(string message, List<string> errorMessages)
    {
        return new ErrorPayload(message, errorMessages);
    }
}