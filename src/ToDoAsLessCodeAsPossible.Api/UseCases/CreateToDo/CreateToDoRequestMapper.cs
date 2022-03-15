using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Mapper;
namespace ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDo;

public class CreateToDoRequestMapper : IRequestToUseCaseMapHandler<CreateToDoRequest, ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo.CreateToDo>
{
    public ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo.CreateToDo Map(CreateToDoRequest request, CancellationToken token)
    {
        var command = new ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo.CreateToDo(request.Title);
        return command;
    }
}