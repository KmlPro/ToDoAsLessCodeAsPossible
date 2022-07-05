using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.Domain.Repository;
using ToDoAsLessCodeAsPossible.Domain.ToDos;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDos;

public record CreateToDo(string Title) : ICommand;

internal class CreateToDoHandler : ICommandHandler<CreateToDo>
{
    private readonly IToDoRepository _toDoRepository;

    public CreateToDoHandler(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public async Task HandleAsync(CreateToDo command, CancellationToken token)
    {
        var toDo = ToDo.Create(command.Title);
        await _toDoRepository.AddAsync(toDo, token);
    }
}