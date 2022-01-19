using ToDoAsLessCodeAsPossible.Domain.Entity;
using ToDoAsLessCodeAsPossible.Domain.Repository;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence.Repositories;

internal class ToDoRepository : IToDoRepository
{
    private readonly ToDoWriteDbContext _writeDbContext;

    public ToDoRepository(ToDoWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task AddAsync(ToDo toDoEntity, CancellationToken token)
    {
        await _writeDbContext.ToDo.AddAsync(toDoEntity, token);
    }
}