using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

//According to https://docs.microsoft.com/en-us/ef/ef6/saving/transactions, BeginTransaction is recommended way to handle transactions, thats  why i use Database.BeginTransaction instead of TransactionScope options
public class TransactionScopeFactory
{
    private DbContext _dbContext;

    public TransactionScopeFactory(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IDbContextTransaction Create()
    {
        return _dbContext.Database.BeginTransaction();
    }
}