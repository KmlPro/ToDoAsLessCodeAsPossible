namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.ErrorHandling.Mapper;

public interface IExceptionToResponseMapper
{
    public ErrorResponse? Map<TExceptionType>(TExceptionType exception) where TExceptionType : Exception;
}