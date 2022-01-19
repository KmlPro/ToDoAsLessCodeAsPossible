using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.StonglyTypedIdConfiguration;

/// <summary>
/// Credits to Kamil Grzybek:  https://github.com/kgrzybek/modular-monolith-with-ddd/blob/90ab9b20a1c6e11700e8b357929c5f792b2b32bf/src/BuildingBlocks/Infrastructure/StronglyTypedIdValueConverterSelector.cs
/// </summary>
internal class StronglyTypedIdValueConverterSelector : ValueConverterSelector
{
    private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters
        = new ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo>();

    public StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
        : base(dependencies)
    {
    }

    public override IEnumerable<ValueConverterInfo> Select(Type? modelClrType, Type? providerClrType = null)
    {
        var baseConverters = base.Select(modelClrType, providerClrType);
        foreach (var converter in baseConverters)
        {
            yield return converter;
        }

        var underlyingModelType = UnwrapNullableType(modelClrType);
        var underlyingProviderType = UnwrapNullableType(providerClrType);

        if (underlyingProviderType is null || underlyingProviderType == typeof(Guid))
        {
            var isTypedIdValue = typeof(EntityId).IsAssignableFrom(underlyingModelType);
            if (!isTypedIdValue) yield break;

            var converterType = typeof(TypedIdValueConverter<>).MakeGenericType(underlyingModelType);

            yield return this._converters.GetOrAdd((underlyingModelType, typeof(Guid)), _ =>
            {
                return new ValueConverterInfo(
                    modelClrType,
                    typeof(Guid),
                    valueConverterInfo =>
                        (ValueConverter)Activator.CreateInstance(converterType,
                            valueConverterInfo.MappingHints));
            });
        }
    }

    private static Type? UnwrapNullableType(Type? type)
    {
        if (type is null)
        {
            return null;
        }

        return Nullable.GetUnderlyingType(type) ?? type;
    }
}