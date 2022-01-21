using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

internal sealed class UnitOfWorkCommandPipelineBehavior : ICommandPipelineBehavior
{
    private ITransactionScopeFactory _transactionScopeFactory;

    public UnitOfWorkCommandPipelineBehavior(ITransactionScopeFactory transactionScopeFactory)
    {
        _transactionScopeFactory = transactionScopeFactory;
    }

    //about ConfigureAwait(false): https://devblogs.microsoft.com/dotnet/configureawait-faq/
    //the reason why i use it here is to improve performance, because there i don't care about synchronization context (like HttpContext.Current)
    public async Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
    {
        await using var transaction = _transactionScopeFactory.Create();
        await next().ConfigureAwait(false);
        await transaction.CommitAsync(cancellationToken);;
    }
}