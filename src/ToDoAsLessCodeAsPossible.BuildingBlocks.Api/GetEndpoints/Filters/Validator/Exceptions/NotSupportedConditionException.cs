namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator.Exceptions;

public class NotSupportedConditionException : Exception
{
    private const string NotSupportedCondition =
        "Not supported Condition. Field Name: {0}, Condition {1}";

    public NotSupportedConditionException(string field, string condition) : base(
        string.Format(NotSupportedCondition, field, condition))
    {
    }
}