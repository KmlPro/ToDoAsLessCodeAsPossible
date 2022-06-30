using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;

namespace ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;

public record GetAllToDo(QueryFilter? QueryFilter) : IQuery<IEnumerable<ToDoDto>>, IWithQueryFilter;