namespace ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Commands;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task HandleAsync(TCommand command, CancellationToken token);
}