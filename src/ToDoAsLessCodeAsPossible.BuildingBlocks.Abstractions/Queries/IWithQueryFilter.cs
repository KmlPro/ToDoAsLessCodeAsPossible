using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IWithQueryFilter
{
    public QueryFilter? QueryFilter { get; }
}