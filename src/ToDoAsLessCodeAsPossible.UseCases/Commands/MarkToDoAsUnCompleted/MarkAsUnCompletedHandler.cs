using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.MarkToDoAsUnCompleted;

public record MarkToDoAsUnCompleted(Guid Id) : ICommand;

internal class MarkToDoAsUnCompletedHandler : ICommandHandler<MarkToDoAsUnCompleted>
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public MarkToDoAsUnCompletedHandler(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task HandleAsync(MarkToDoAsUnCompleted command, CancellationToken token)
    {
        var entityId = EntityId.Create(command.Id);
        
        var toDo = await _toDoWriteRepository.GetAsync(entityId, token);
        toDo.MarkAsUnCompleted();
    }
}