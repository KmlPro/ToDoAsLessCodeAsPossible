using ToDoAsLessCodeAsPossible.Domain.Entity;

namespace ToDoAsLessCodeAsPossible.Domain.Repository;

//i think it is not right place for this interface, need to change it
public interface IToDoRepository
{
    public Task AddAsync(ToDo toDoEntity, CancellationToken token);
    public Task<bool> Exists(string title, CancellationToken token);
}