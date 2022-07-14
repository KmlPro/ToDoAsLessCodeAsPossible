using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.Domain.ToDos;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDos;

public record CreateToDo(string Title) : ICommand;

internal class CreateToDoHandler : ICommandHandler<CreateToDo>
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public CreateToDoHandler(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task<CommandResult> HandleAsync(CreateToDo command, CancellationToken token)
    {
        var toDo = ToDo.Create(command.Title);
        await _toDoWriteRepository.AddAsync(toDo, token);
        
        return new CommandResult(toDo.Id.Value.ToString());
    }
}