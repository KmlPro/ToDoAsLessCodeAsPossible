using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.StonglyTypedIdConfiguration;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory.DatabaseConfiguration;

internal class InMemoryEfDatabaseConfigurationFactory
{
    public Action<DbContextOptionsBuilder> Create()
    {
        return builder =>
        {
            builder.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();
            builder.UseInMemoryDatabase("IncidentReport");
        };
    }
}