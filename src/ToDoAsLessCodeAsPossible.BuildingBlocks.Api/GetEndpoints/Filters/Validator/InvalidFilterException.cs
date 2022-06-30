namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator;

public class InvalidFilterException : Exception
{
    private const string FieldIsNotSupportedOrInvalidCondition = "Field is not filterable or invalid operator. Field Name: ";
    
    public InvalidFilterException(string field): base(FieldIsNotSupportedOrInvalidCondition + field) { }    
}