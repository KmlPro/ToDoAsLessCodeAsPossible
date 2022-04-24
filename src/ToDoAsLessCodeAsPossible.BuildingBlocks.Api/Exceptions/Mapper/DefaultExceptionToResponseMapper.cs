using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Payload;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Mapper;

internal class DefaultExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ErrorResponse? Map<TExceptionType>(TExceptionType exception) where TExceptionType : Exception
        => exception switch
        {
            //kbytner 24.04.2022 - add validation error here
        //    InvalidRequestException ex => new ErrorResponse(HttpStatusCode.BadRequest,CreatePayload(ex.Message, ex.ErrorMessages)),
            _ => null
        };

    private ErrorPayload CreatePayload(string message, List<string> errorMessages)
    {
        return new ErrorPayload(message, errorMessages);
    }
}