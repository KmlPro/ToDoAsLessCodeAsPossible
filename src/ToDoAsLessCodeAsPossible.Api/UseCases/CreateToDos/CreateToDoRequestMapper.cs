using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;

public class CreateToDoRequestMapper : IRequestToUseCaseMapHandler<CreateToDoRequest, CreateToDo>
{
    public CreateToDo Map(CreateToDoRequest request, CancellationToken token)
    {
        var command = new CreateToDo(request.Title);
        return command;
    }
}