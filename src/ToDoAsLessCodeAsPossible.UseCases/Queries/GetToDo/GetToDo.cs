using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.UseCases.Dtos;

namespace ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

public record GetToDo(Guid Id) : IQuery<ToDoDto>;