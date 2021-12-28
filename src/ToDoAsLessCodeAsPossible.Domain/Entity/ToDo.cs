using EnsureThat;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Domain.ValueObjects;

namespace ToDoAsLessCodeAsPossible.Domain.Entity;

public class ToDo
{
    public EntityId Id { get; private set; }
    public string Title{ get; private set; }
    public bool IsCompleted { get; private set; }
    
    private ToDo(EntityId id,string title)
    {
        Ensure.That(title, nameof(title)).IsNotNullOrWhiteSpace();
        
        Id = id;
        Title = title;
        IsCompleted = false;
    }

    public static ToDo Create(string title)
    {
        var id = EntityId.Create();
        return new ToDo(id, title);
    }
}