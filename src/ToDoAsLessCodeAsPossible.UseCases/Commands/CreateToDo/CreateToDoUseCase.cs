using ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Handlers.Commands;
using ToDoAsLessCodeAsPossible.Domain.Entity;
using ToDoAsLessCodeAsPossible.Domain.Repository;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDo;

public record CreateToDo(string Title) : ICommand;

public class CreateToDoUseCase : ICommandHandler<CreateToDo>
{
    private IToDoRepository _toDoRepository;

    public CreateToDoUseCase(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task Handle(CreateToDo command, CancellationToken token)
    {
        var toDo = ToDo.Create(command.Title);
        await _toDoRepository.AddAsync(toDo, token);
    }
}