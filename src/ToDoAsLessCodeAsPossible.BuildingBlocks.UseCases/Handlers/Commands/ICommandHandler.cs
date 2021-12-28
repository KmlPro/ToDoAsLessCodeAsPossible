namespace ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Handlers.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task Handle(TCommand command, CancellationToken token);
}