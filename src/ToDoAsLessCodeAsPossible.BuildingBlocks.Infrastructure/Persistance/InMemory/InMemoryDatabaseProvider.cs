namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;

/// <summary>
/// For now it is possible to choose two database providers
/// <list type="bullet">
/// <item>
/// <description>Sqlite - it is more close to real relational database (ex. checking constrains like foreign keys, data consistency) </description>
/// </item>
/// <item>
/// <description>EfCore - this is simple database provided by Ef Core team, but it is not checking database constraints </description>
/// </item>
/// </list>
/// </summary>
public enum InMemoryDatabaseProvider
{
    Sqlite,
    EfCore
}