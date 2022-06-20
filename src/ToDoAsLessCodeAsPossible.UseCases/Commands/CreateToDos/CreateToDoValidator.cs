using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.Domain.Repository;

namespace ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDos;

public class CreateToDoValidator : ICommandStructValidator<CreateToDo>, ICommandRulesValidator<CreateToDo>
{
    private const string TitleCannotBeEmpty = "Title can not be empty string";
    private const string TitleShouldContainsMoreThanCharacters = "Title can contains more than 5 characters";
    private const string ToDoWithSameTitleAlreadyExists = "Title with exactly same title already exist";

    private readonly IToDoRepository _toDoRepository;

    public CreateToDoValidator(IToDoRepository toDoRepository)
    {
        _toDoRepository = toDoRepository;
    }

    public List<string> ValidateStruct(CreateToDo query)
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

    public async Task<List<string>> ValidateUseCaseRules(CreateToDo command, CancellationToken cancellationToken)
    {
        var errors = new List<string>();

        var toDoWithSameTitleExists = await _toDoRepository.Exists(command.Title, cancellationToken);
        if (toDoWithSameTitleExists)
        {
            errors.Add(ToDoWithSameTitleAlreadyExists);
        }

        return errors;
    }
}