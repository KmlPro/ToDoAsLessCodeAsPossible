namespace ToDoAsLessCodeAsPossible.Domain.ValueObjects;

public class EntityId
{
    public Guid Value { get; }
    
    private EntityId(Guid guid)
    {
        Value = guid;
    }

    public static EntityId Create()
    {
        return new EntityId(Guid.NewGuid());
    }
}