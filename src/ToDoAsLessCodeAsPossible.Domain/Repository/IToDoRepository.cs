using ToDoAsLessCodeAsPossible.Domain.Entity;

namespace ToDoAsLessCodeAsPossible.Domain.Repository;

public interface IToDoRepository
{
    public Task AddAsync(ToDo toDoEntity, CancellationToken token);
}