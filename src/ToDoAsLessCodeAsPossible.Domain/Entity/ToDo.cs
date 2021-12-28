namespace ToDoAsLessCodeAsPossible.Domain.Entity;

public class ToDo
{
    public Guid Id { get; private set; }
    public string Title{ get; private set; }
    public bool IsCompleted { get; private set; }
    
    private ToDo(Guid id,string title)
    {
        Id = id;
        Title = title;
        IsCompleted = false;
    }

    public ToDo Create(string title)
    {
        var id = Guid.NewGuid();
        return new ToDo(id, title);
    }
}