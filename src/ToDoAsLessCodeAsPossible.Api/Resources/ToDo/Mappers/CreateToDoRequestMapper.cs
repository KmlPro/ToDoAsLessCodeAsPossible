using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;

namespace ToDoAsLessCodeAsPossible.Api.Resources.ToDo.Mappers;

public class CreateToDoRequestMapper : IRequestToUseCaseMapHandler<ToDoRequests.CreateToDoRequest, CreateToDo>
{
    public CreateToDo Map(ToDoRequests.CreateToDoRequest request, CancellationToken token)
    {
        var command = new CreateToDo(request.Title);
        return command;
    }
}