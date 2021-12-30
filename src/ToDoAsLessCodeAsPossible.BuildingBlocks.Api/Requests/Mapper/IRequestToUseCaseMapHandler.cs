namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

public interface IRequestToUseCaseMapHandler<in TRequest, out TUseCase>
{
    public TUseCase Map(TRequest request, CancellationToken token);
}