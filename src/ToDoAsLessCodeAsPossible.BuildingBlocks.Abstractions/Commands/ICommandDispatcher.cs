namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandDispatcher
{
    public Task<CommandResult> SendAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : class, ICommand;
}