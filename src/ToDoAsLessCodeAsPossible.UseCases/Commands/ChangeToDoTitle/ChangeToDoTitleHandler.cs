using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.ChangeToDoTitle;

public record ChangeToDoTitle(Guid Id, string Title) : ICommand;

internal class ChangeToDoTitleHandler : ICommandHandler<ChangeToDoTitle>
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public ChangeToDoTitleHandler(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task HandleAsync(ChangeToDoTitle command, CancellationToken token)
    {
        var entityId = EntityId.Create(command.Id);
        
        var toDo = await _toDoWriteRepository.GetAsync(entityId, token);
        toDo.ChangeTitle(command.Title);
    }
}