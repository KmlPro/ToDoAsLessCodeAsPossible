using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.CompleteToDos;

public record CompleteToDo(Guid Id) : ICommand;

internal class CompleteToDoHandler : ICommandHandler<CompleteToDo>
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public CompleteToDoHandler(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task<CommandResult> HandleAsync(CompleteToDo command, CancellationToken token)
    {
        var entityId = new EntityId(command.Id);
        
        var toDo = await _toDoWriteRepository.GetAsync(entityId, token);
        toDo.Complete();
        
        return new CommandResult(entityId.Value.ToString());
    }
}