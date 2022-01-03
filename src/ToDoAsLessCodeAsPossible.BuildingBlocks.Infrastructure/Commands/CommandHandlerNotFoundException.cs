namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

public class CommandHandlerNotFoundException : Exception
{
    public CommandHandlerNotFoundException(string commandType) : base(
        $"Command Handler not found for command '{commandType}'") { }
}