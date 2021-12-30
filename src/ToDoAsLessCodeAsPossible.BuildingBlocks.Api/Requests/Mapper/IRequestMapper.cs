namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

public interface IRequestMapper
{
    public TUseCase Map<TRequest, TUseCase>(TRequest request, CancellationToken cancellationToken);
}