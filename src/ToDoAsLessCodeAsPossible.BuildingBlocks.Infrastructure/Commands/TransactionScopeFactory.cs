using System.Transactions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

//TO DO I need to rewirte it to use context.Database.BeginTransaction
//According to https://docs.microsoft.com/en-us/ef/ef6/saving/transactions
public class TransactionScopeFactory
{
    public TransactionScope Create()
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}