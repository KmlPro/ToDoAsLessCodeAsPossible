using ToDoAsLessCodeAsPossible.Domain.Entity;
using ToDoAsLessCodeAsPossible.Domain.Repository;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Persistence.Repositories;

public class ToDoRepository : IToDoRepository
{
    public async Task AddAsync(ToDo toDoEntity, CancellationToken token)
    {
    }
}