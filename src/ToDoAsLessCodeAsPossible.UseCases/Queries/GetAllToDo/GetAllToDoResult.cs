using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;

namespace ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDos;

public record GetAllToDoResult(IEnumerable<ToDoDto> ToDos) : IQueryResult;