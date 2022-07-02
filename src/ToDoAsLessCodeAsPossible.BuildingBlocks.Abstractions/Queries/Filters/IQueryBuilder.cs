namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;

public interface IQueryBuilder
{
    /// <summary>
    /// Add mapping between field name from query to database field
    /// </summary>
    /// <param name="queryField">Field name from query filter</param>
    /// <param name="databaseField">Database field name</param>
    /// <returns>Instance of IQueryFilterCompiler</returns>
    public IQueryBuilder WithFieldMapping(string queryField, string databaseField);

    /// <summary>
    /// Add base query (retrieval, joining part etc)
    /// </summary>
    /// <param name="query">Database query</param>
    /// <returns>Instance of IQueryFilterCompiler</returns>
    public IQueryBuilder WithBaseQuery(string query);
    
    /// <summary>
    /// Add query filter
    /// </summary>
    /// <param name="queryFilter">Object used for generate database query</param>
    /// <returns>Instance of IQueryFilterCompiler</returns>
    public IQueryBuilder WithQueryFilter(QueryFilter queryFilter);

    /// <summary>
    /// Compile Query Filter to filter database query.
    /// </summary>
    /// <returns>Database Query</returns>
    public string Build();
}