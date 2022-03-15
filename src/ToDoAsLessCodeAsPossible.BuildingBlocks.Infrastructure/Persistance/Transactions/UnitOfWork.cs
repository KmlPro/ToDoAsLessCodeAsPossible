using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

//According to https://docs.microsoft.com/en-us/ef/ef6/saving/transactions, BeginTransaction is recommended way to handle transactions, thats  why i use Database.BeginTransaction instead of TransactionScope options
public class UnitOfWork : IUnitOfWork
{
    private DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await _dbContext.SaveChangesAsync(token);
    }
}