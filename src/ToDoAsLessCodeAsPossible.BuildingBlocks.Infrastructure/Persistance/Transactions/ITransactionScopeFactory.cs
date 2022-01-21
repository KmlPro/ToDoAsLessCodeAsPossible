using Microsoft.EntityFrameworkCore.Storage;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

internal interface ITransactionScopeFactory
{
    public IDbContextTransaction Create();
}