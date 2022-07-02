namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters.Exceptions;

public class MappingForFieldNotFoundException : Exception
{
    private const string ErrorMessage = "Mapping for field not found. Field name: ";

    public MappingForFieldNotFoundException(string fieldName) : base(ErrorMessage + fieldName)
    {
    }
}