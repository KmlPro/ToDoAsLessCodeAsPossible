using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.ChangeToDoTitles;

public record ChangeToDoTitle(Guid Id, string Title) : ICommand;

internal class ChangeToDoTitleHandler : ICommandHandler<ChangeToDoTitle>
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public ChangeToDoTitleHandler(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task<CommandResult> HandleAsync(ChangeToDoTitle command, CancellationToken token)
    {
        var entityId = new EntityId(command.Id);
        
        var toDo = await _toDoWriteRepository.GetAsync(entityId, token);
        toDo.ChangeTitle(command.Title);

        return new CommandResult(entityId.Value.ToString());
    }
}