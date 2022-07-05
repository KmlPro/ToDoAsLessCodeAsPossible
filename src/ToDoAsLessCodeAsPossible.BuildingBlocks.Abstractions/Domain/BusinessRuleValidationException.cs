namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

public abstract class BusinessRuleValidationException : Exception
{
    public BusinessRuleValidationException(IBusinessRule brokenRule, string message) : base(message)
    {
        BrokenRule = brokenRule;
        Details = message;
    }

    public IBusinessRule BrokenRule { get; }
    private string Details { get; }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {Details}";
    }
}