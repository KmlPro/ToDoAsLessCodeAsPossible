using Microsoft.EntityFrameworkCore;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory.DatabaseConfiguration;

internal class InMemoryEfDatabaseConfigurationFactory
{
    public Action<DbContextOptionsBuilder> Create()
    {
        return options =>
        {
            options.UseInMemoryDatabase("IncidentReport");
        };
    }
}