namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;

public interface IWithQueryFilter
{
    public QueryFilter? QueryFilter { get; }
}