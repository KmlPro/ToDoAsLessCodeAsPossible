using EnsureThat;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Domain.ValueObjects;

namespace ToDoAsLessCodeAsPossible.Domain.Entity;

public class ToDo
{
    public EntityId Id { get; }
    private string _title;
    private bool _isCompleted;

    private ToDo(EntityId id, string title)
    {
        Ensure.That(title, nameof(title)).IsNotNullOrWhiteSpace();

        Id = id;
        _title = title;
        _isCompleted = false;
    }

    public static ToDo Create(string title)
    {
        var id = EntityId.Create();
        return new ToDo(id, title);
    }
}