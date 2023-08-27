using ToDoAsLessCodeAsPossible.Domain.ToDos;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Tests.Queries.GetAllToDoHandler;

public class TestFixture
{
    private ToDoWriteDbContext _dbContext;

    internal TestFixture(ToDoWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAndSaveInDbRandomToDos(int count)
    {
        var toDos = new List<ToDo>();
        for (int i = 0; i < count; i++)
        {
            toDos.Add(ToDo.Create(count.ToString()));
        }

        _dbContext.AddRange(toDos);
        _dbContext.SaveChanges();
    }
}