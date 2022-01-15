namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public delegate Task CommandHandlerDelegate();

public interface ICommandPipelineBehavior
{
    Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next)
        where TCommand : ICommand;
}