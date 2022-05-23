using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

public class ValidationCommandPipelineBehavior: ICommandPipelineBehavior
{
    public async Task HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
    {
        await next().ConfigureAwait(false);
    }
}