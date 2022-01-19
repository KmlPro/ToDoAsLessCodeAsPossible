using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.StonglyTypedIdConfiguration;

/// <summary>
/// Credits to Kamil Grzybek: https://github.com/kgrzybek/modular-monolith-with-ddd/blob/90ab9b20a1c6e11700e8b357929c5f792b2b32bf/src/BuildingBlocks/Infrastructure/TypedIdValueConverter.cs
/// </summary>
internal class TypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
    where TTypedIdValue : EntityId
{
    public TypedIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(id => id.Value, value => Create(value), mappingHints)
    {
    }

    private static TTypedIdValue Create(Guid id)
    {
        return Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
    }
}