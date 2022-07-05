using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

namespace ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantUnCompleteAlreadyUnCompletedToDo;

public class CantUnCompleteAlreadyUnCompletedToDoException : BusinessRuleValidationException
{
    private const string ErrorMessage = "Cant complete To do item that is already completed";

    public CantUnCompleteAlreadyUnCompletedToDoException(IBusinessRule brokenRule) : base(brokenRule, ErrorMessage)
    {
    }
}