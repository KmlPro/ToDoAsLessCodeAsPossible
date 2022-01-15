using System.Transactions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

public class TransactionScopeFactory
{
    public TransactionScope Create()
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}