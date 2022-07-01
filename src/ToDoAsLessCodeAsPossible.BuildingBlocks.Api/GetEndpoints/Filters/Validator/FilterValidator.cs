using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator;

public static class FilterValidator
{
    public static void ValidateAndThrow(FilterDefinition filterDefinition, QueryFilter? queryFilter)
    {
        if (queryFilter == null)
        {
            return;
        }

        foreach (var filter in queryFilter.Filters)
        {
            if (FilterNotExistsInDefinition(filterDefinition, filter))
            {
                throw new FieldIsNoFilterableException(filter.FieldName);
            }

            var filterFieldDefinition = filterDefinition.FilterableFields.First(x => x.Name == filter.FieldName);

            if (ConditionIsNotSupported(filterFieldDefinition, filter))
            {
                throw new NotSupportedConditionException(filter.FieldName, filter.Condition);
            }
        }
    }

    private static bool ConditionIsNotSupported(FilterDefinitionField filterFieldDefinition, QueryFilterField filter)
    {
        return filterFieldDefinition.AvailableConditions.Any(condition => condition != filter.Condition);
    }

    private static bool FilterNotExistsInDefinition(FilterDefinition filterDefinition, QueryFilterField filter)
    {
        return !filterDefinition.FilterableFields.Exists(x => x.Name == filter.FieldName);
    }
}