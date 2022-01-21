using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

internal sealed class UnitOfWorkCommandPipelineBehavior : ICommandPipelineBehavior
{
    private TransactionScopeFactory _transactionScopeFactory;

    public UnitOfWorkCommandPipelineBehavior(TransactionScopeFactory transactionScopeFactory)
    {
        _transactionScopeFactory = transactionScopeFactory;
    }

    //about ConfigureAwait(false): https://devblogs.microsoft.com/dotnet/configureawait-faq/
    //the reason why i use it here is to improve performance, because there i don't care about synchronization context (like HttpContext.Current)
    public async Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
    {
        using var transaction = _transactionScopeFactory.Create();
        await next().ConfigureAwait(false);
        await transaction.CommitAsync(cancellationToken);;
    }
}