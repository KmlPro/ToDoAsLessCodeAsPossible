namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

public abstract class WithCheckRule
{
    protected void CheckRule(IBusinessRule? rule)
    {
        rule?.CheckIsBroken();
    }
}