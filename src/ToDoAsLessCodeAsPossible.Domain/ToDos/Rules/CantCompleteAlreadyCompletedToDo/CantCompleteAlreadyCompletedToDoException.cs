using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

namespace ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantCompleteAlreadyCompletedToDo;

public class CantCompleteAlreadyCompletedToDoException : BusinessRuleValidationException
{
    private const string ErrorMessage = "Cant complete To do item that is already completed";

    public CantCompleteAlreadyCompletedToDoException(IBusinessRule businessRule) : base(businessRule, ErrorMessage)
    {
    }
}