using EnsureThat;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantCompleteAlreadyCompletedToDo;
using ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantMarkAsUncompletedAlreadyUncompleted;

namespace ToDoAsLessCodeAsPossible.Domain.ToDos;

public class ToDo : Entity
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

    public void Complete()
    {
        CheckRule(new CantCompleteAlreadyCompletedToDoRule(_isCompleted));
        _isCompleted = true;
    }

    public void MarkAsUnCompleted()
    {
        CheckRule(new CantMarkAsUncompletedAlreadyUncompletedRule(_isCompleted));
        _isCompleted = false;
    }

    public void ChangeTitle(string title)
    {
        Ensure.That(title, nameof(title)).IsNotNullOrWhiteSpace();
        _title = title;
    }
}