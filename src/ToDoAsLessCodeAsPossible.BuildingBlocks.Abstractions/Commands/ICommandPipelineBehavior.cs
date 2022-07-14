namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public delegate Task<CommandResult> CommandHandlerDelegate();

public interface ICommandPipelineBehavior
{
    Task<CommandResult> HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next)
        where TCommand : ICommand;
}