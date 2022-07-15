using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.MarkToDoAsUnCompleted;

public class MarkAsUncompletedValidator : ICommandRulesValidator<MarkToDoAsUncompleted>
{
    private const string ToDoNotExists = "To Do with given id does not exist or has been deleted. Id: ";

    private readonly IToDoWriteRepository _toDoWriteRepository;

    public MarkAsUncompletedValidator(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task<List<string>> ValidateUseCaseRules(MarkToDoAsUncompleted command, CancellationToken cancellationToken)
    {
        var errors = new List<string>();

        var entityId = new EntityId(command.Id);
        var isExists = await _toDoWriteRepository.ExistsAsync(entityId, cancellationToken);
        if (!isExists)
        {
            errors.Add(ToDoNotExists + command.Id);
        }

        return errors;
    }
}