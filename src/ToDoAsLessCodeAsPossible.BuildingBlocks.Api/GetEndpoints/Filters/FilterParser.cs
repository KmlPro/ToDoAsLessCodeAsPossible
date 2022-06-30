using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters;

public static class FilterParser
{
    /// <summary>
    /// Return null if filters param is null or empty
    /// </summary>
    /// <param name="filters"></param>
    /// <returns></returns>
    public static QueryFilter? Parse(string? filters)
    {
        if (string.IsNullOrEmpty(filters))
            return null;
        
        var filterFields = new List<QueryFilterField>();
        
        var splittedFilters = filters.Split("&");
        //TO DO Validation
        
        foreach (var filter in splittedFilters)
        {
            var indexOfBeginSquareBrackets = filter.IndexOf("[", StringComparison.CurrentCulture);
            var indexOfEndSquareBrackets = filter.IndexOf("]", StringComparison.CurrentCulture);
            var indexOfEqualMark = filter.IndexOf("=", StringComparison.CurrentCulture);
            var indexOfOperatorNameStarts = indexOfBeginSquareBrackets + 1;
            var indexOfFilterValueStarts = indexOfEqualMark + 1;

            var fieldNameLenght = indexOfBeginSquareBrackets;
            var conditionLenght = indexOfEndSquareBrackets - indexOfBeginSquareBrackets - 1;
            var filterValueLenght = filter.Length - indexOfFilterValueStarts;

            var fieldName = filter.Substring(0, fieldNameLenght);
            var operatorName = filter.Substring(indexOfOperatorNameStarts, conditionLenght);
            var filterValue = filter.Substring(indexOfFilterValueStarts, filterValueLenght);
            
            filterFields.Add(new QueryFilterField(fieldName, operatorName, filterValue));
        }

        return new QueryFilter(filterFields);
    }
}