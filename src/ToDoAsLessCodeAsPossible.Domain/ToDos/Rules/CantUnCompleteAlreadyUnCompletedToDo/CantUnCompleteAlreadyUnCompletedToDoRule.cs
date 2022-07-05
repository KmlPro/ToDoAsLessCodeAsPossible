using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

namespace ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantUnCompleteAlreadyUnCompletedToDo;

public class CantUnCompleteAlreadyUnCompletedToDoRule : IBusinessRule
{
    private readonly bool _isCompleted;
    
    public CantUnCompleteAlreadyUnCompletedToDoRule(bool isCompleted)
    {
        _isCompleted = isCompleted;
    }
    
    public void CheckIsBroken()
    {
        if (!_isCompleted)
        {
            throw new CantUnCompleteAlreadyUnCompletedToDoException(this);
        }
    }
}