using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints;

public static class FilterParser
{
    public static QueryFilter Parse(string filters)
    {
        var filterFields = new List<FilterField>();
        
        var splittedFilters = filters.Split("&");
        //TO DO Validation
        
        foreach (var filter in splittedFilters)
        {
            var indexOfBeginSquareBrackets = filter.IndexOf("[", StringComparison.CurrentCulture);
            var indexOfEndSquareBrackets = filter.IndexOf("]", StringComparison.CurrentCulture);
            var indexOfEqualMark = filter.IndexOf("=", StringComparison.CurrentCulture);
            var indexOfOperatorNameStarts = indexOfBeginSquareBrackets + 1;
            var indexOfFilterValueStarts = indexOfEqualMark + 1;

            var fieldNameLenght = indexOfBeginSquareBrackets + 1;
            var operatorLenght = indexOfEndSquareBrackets - indexOfBeginSquareBrackets;
            var filterValueLenght = filter.Length - indexOfFilterValueStarts;

            var fieldName = filter.Substring(0, fieldNameLenght);
            var operatorName = filter.Substring(indexOfOperatorNameStarts, operatorLenght);
            var filterValue = filter.Substring(indexOfFilterValueStarts, filterValueLenght);
            
            filterFields.Add(new FilterField(fieldName, operatorName, filterValue));
        }

        return new QueryFilter(filterFields);
    }
}