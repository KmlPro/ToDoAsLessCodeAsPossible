using ToDoAsLessCodeAsPossible.Domain.Entity;

namespace ToDoAsLessCodeAsPossible.Domain.Repository;

public interface IToDoRepository
{
    public Task<ToDo> GetById();
}