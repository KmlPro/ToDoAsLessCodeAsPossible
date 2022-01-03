namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Exceptions.Mapper;

public interface IExceptionToResponseMapper
{
    public ErrorResponse? Map<TExceptionType>(TExceptionType exception) where TExceptionType : Exception;
}