namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<CommandResult> HandleAsync(TCommand command, CancellationToken token);
}