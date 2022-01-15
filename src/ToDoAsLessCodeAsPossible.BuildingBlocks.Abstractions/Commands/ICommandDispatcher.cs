namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandDispatcher
{
    public Task SendAsync<T>(T command, CancellationToken token) where T : class, ICommand;
}