using System.Net;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Payload;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Exceptions;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Mapper;

internal class DefaultExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ErrorResponse? Map<TExceptionType>(TExceptionType exception) where TExceptionType : Exception
        => exception switch
        {
            InvalidQueryStructException ex => new ErrorResponse(HttpStatusCode.BadRequest,CreatePayload(ex.Message, ex.ErrorMessages)),
            QueryRulesBrokenException ex => new ErrorResponse(HttpStatusCode.UnprocessableEntity,CreatePayload(ex.Message, ex.ErrorMessages)),
            InvalidCommandStructException ex => new ErrorResponse(HttpStatusCode.BadRequest,CreatePayload(ex.Message, ex.ErrorMessages)),
            CommandRulesBrokenException ex => new ErrorResponse(HttpStatusCode.UnprocessableEntity,CreatePayload(ex.Message, ex.ErrorMessages)),
            _ => null
        };

    private ErrorPayload CreatePayload(string message, List<string> errorMessages)
    {
        return new ErrorPayload(message, errorMessages);
    }
}