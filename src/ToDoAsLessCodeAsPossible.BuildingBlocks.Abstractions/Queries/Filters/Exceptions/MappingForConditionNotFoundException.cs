namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters.Exceptions;

public class MappingForConditionNotFoundException : Exception
{
    private const string ErrorMessage = "Mapping for condition not found. Condition value: ";

    public MappingForConditionNotFoundException(string condition) : base(ErrorMessage + condition)
    {
    }
}