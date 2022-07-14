using EnsureThat;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantCompleteAlreadyCompletedToDo;
using ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantMarkAsUncompletedAlreadyUncompleted;

namespace ToDoAsLessCodeAsPossible.Domain.ToDos;

public class ToDo : Entity
{
    public EntityId Id { get; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }

    private ToDo(EntityId id, string title)
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

    public void Complete()
    {
        CheckRule(new CantCompleteAlreadyCompletedToDoRule(IsCompleted));
        IsCompleted = true;
    }

    public void MarkAsUnCompleted()
    {
        CheckRule(new CantMarkAsUncompletedAlreadyUncompletedRule(IsCompleted));
        IsCompleted = false;
    }

    public void ChangeTitle(string title)
    {
        Ensure.That(title, nameof(title)).IsNotNullOrWhiteSpace();
        Title = title;
    }
}