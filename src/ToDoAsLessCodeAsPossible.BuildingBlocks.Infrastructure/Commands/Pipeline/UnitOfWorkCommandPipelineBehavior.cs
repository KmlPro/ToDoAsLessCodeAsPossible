using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

internal sealed  class UnitOfWorkCommandPipelineBehavior : ICommandPipelineBehavior
{
    private TransactionScopeFactory _transactionScopeFactory;

    public UnitOfWorkCommandPipelineBehavior(TransactionScopeFactory transactionScopeFactory)
    {
        _transactionScopeFactory = transactionScopeFactory;
    }

    public async Task Handle<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
    {
        using var transaction = _transactionScopeFactory.Create();
        await next(); //.ConfigureAwait(false); TO DO
        transaction.Complete();;
    }
}