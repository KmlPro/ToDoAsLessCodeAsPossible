using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

public class ValidationCommandPipelineBehavior: ICommandPipelineBehavior
{
    public async Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
    {
        //to do get validator
        await next().ConfigureAwait(false);
    }
}