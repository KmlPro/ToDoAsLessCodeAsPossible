namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;

public class InMemoryDatabaseParameters
{
    public InMemoryDatabaseParameters(InMemoryDatabaseProvider databaseProvider)
    {
        DatabaseProvider = databaseProvider;
    }

    public InMemoryDatabaseProvider DatabaseProvider { get; }
}