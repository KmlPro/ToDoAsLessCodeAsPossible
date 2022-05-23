namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

public class QueryRulesBrokenException : Exception
{
    public QueryRulesBrokenException(List<string> errorMessages) : base("One or more business rules broken")
    {
        ErrorMessages = errorMessages;
    }

    public List<string> ErrorMessages { get; }
}