using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;

namespace ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;

public record GetAllToDoResult(IEnumerable<ToDoDto> ToDos);