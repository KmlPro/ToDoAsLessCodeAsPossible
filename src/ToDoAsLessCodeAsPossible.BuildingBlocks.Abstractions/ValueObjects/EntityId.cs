namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;

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
    
    public static EntityId Create(Guid id)
    {
        return new EntityId(id);
    }
}