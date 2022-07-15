using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.MarkToDoAsUnCompleted;

public record MarkToDoAsUncompleted(Guid Id) : ICommand;

internal class MarkToDoAsUncompletedHandler : ICommandHandler<MarkToDoAsUncompleted>
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public MarkToDoAsUncompletedHandler(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task<CommandResult> HandleAsync(MarkToDoAsUncompleted command, CancellationToken token)
    {
        var entityId = new EntityId(command.Id);
        
        var toDo = await _toDoWriteRepository.GetAsync(entityId, token);
        toDo.MarkAsUnCompleted();
        
        return new CommandResult(toDo.Id.Value.ToString());
    }
}