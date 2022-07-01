namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator.Exceptions;

public class FieldIsNoFilterableException: Exception
{
    private const string FieldIsNoFilterable = "Field is not filterable. Field Name: ";
    
    public FieldIsNoFilterableException(string field): base(FieldIsNoFilterable + field) { }    
}