using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.ChangeToDoTitles;

public class ChangeToDoTitleValidator : ICommandRulesValidator<ChangeToDoTitle>, ICommandStructValidator<ChangeToDoTitle>
{
    private const string ToDoNotExists = "To Do with given id does not exist or has been deleted. Id: ";
    private const string TitleCannotBeEmpty = "Title can not be empty string";
    private const string TitleShouldContainsMoreThanCharacters = "Title can contains more than 5 characters";
    private const string ToDoWithSameTitleAlreadyExists = "Title with exactly same title already exist";

    private readonly IToDoWriteRepository _toDoWriteRepository;

    public ChangeToDoTitleValidator(IToDoWriteRepository toDoWriteRepository)
    {
        _toDoWriteRepository = toDoWriteRepository;
    }

    public async Task<List<string>> ValidateUseCaseRules(ChangeToDoTitle command, CancellationToken cancellationToken)
    {
        var errors = new List<string>();

        var entityId = new EntityId(command.Id);
        var isExists = await _toDoWriteRepository.ExistsAsync(entityId, cancellationToken);
        if (!isExists)
        {
            errors.Add(ToDoNotExists + command.Id);
        }
        
        var toDoWithSameTitleExists = await _toDoWriteRepository.ExistsAsync(command.Title, cancellationToken);
        if (toDoWithSameTitleExists)
        {
            errors.Add(ToDoWithSameTitleAlreadyExists);
        }

        return errors;
    }

    public List<string> ValidateStruct(ChangeToDoTitle query)
    {
        var errors = new List<string>();
        
        if (string.IsNullOrEmpty(query.Title))
        {
            errors.Add(TitleCannotBeEmpty);
        }
        else if (query.Title.Length < 5)
        {
            errors.Add(TitleShouldContainsMoreThanCharacters);
        }

        return errors;
    }
}