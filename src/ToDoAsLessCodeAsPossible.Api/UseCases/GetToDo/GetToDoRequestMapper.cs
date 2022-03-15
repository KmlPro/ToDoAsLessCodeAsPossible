using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetToDo;

public class GetToDoRequestMapper : IRequestToUseCaseMapHandler<GetToDoRequest, ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo.GetToDo>
{
    public ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo.GetToDo Map(GetToDoRequest request, CancellationToken token)
    {
        var query = new ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo.GetToDo(request.Id);
        return query;
    }
}