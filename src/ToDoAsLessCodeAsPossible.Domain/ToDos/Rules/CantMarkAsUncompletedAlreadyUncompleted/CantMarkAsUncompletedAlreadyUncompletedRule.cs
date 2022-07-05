using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

namespace ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantMarkAsUncompletedAlreadyUncompleted;

public class CantMarkAsUncompletedAlreadyUncompletedRule : IBusinessRule
{
    private readonly bool _isCompleted;
    
    public CantMarkAsUncompletedAlreadyUncompletedRule(bool isCompleted)
    {
        _isCompleted = isCompleted;
    }
    
    public void CheckIsBroken()
    {
        if (!_isCompleted)
        {
            throw new CantMarkAsUncompletedAlreadyUncompletedException(this);
        }
    }
}