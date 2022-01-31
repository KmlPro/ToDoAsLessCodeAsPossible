namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandDispatcher
{
    public Task SendAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : class, ICommand;
}