namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Exceptions;

public class CommandRulesBrokenException : Exception
{
    public CommandRulesBrokenException(List<string> errorMessages) : base("One or more business rules broken")
    {
        ErrorMessages = errorMessages;
    }

    public List<string> ErrorMessages { get; }
}