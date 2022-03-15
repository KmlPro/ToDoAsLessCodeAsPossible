using Microsoft.EntityFrameworkCore.Storage;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

internal interface IUnitOfWork
{
    public IDbContextTransaction BeginTransaction();
    public Task SaveChangesAsync(CancellationToken token);
}