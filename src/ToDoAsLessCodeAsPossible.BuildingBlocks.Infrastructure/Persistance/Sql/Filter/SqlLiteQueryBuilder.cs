using System.Text;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql.Filter;

//kbytner 2.07.2022 - it is only mvp, there is a lack of field type, enums etc
public class SqlLiteQueryBuilder : IQueryBuilder
{
    private readonly Dictionary<string, string> ConditionMappings = new()
        { { "eq", "=" } };
    
    private Dictionary<string, string> FieldMappings { get; }
    private string BaseQuery { get; set; }
    private QueryFilter QueryFilter { get; set; }

    public SqlLiteQueryBuilder()
    {
        FieldMappings = new Dictionary<string, string>();
        QueryFilter = new QueryFilter(new List<QueryFilterField>());
        BaseQuery = string.Empty;
    }

    public IQueryBuilder WithFieldMapping(string queryField, string databaseField)
    {
        FieldMappings.Add(queryField, databaseField);
        return this;
    }

    public IQueryBuilder WithBaseQuery(string query)
    {
        BaseQuery = query;
        return this;
    }

    public IQueryBuilder WithQueryFilter(QueryFilter queryFilter)
    {
        QueryFilter = queryFilter;
        return this;
    }

    public string Build()
    {
        var sb = new StringBuilder();
        sb.Append(BaseQuery);
        if (QueryFilter.Filters.Any())
        {
            sb.Append(" WHERE");
        }
        
        foreach (var queryFilter in QueryFilter.Filters)
        {
            var databaseCondition = GetCondition(queryFilter);
            var databaseField = GetDatabaseField(queryFilter);
            sb.Append($" {databaseField} {databaseCondition} {queryFilter.FilterValue}");
        }

        return sb.ToString();
    }

    private string GetCondition(QueryFilterField filterField)
    {
        var success = ConditionMappings.TryGetValue(filterField.Condition, out string? mappedCondition);
        if (!success || string.IsNullOrEmpty(mappedCondition))
        {
            throw new MappingForConditionNotFoundException(filterField.Condition);
        }

        return mappedCondition;
    }
    
    private string GetDatabaseField(QueryFilterField filterField)
    {
        var success = FieldMappings.TryGetValue(filterField.FieldName, out string? mappedDatabaseField);
        if (!success || string.IsNullOrEmpty(mappedDatabaseField))
        {
            throw new MappingForFieldNotFoundException(filterField.FieldName);
        }

        return mappedDatabaseField;
    }
}