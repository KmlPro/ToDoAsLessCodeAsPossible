using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

namespace ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

public record GetToDo(Guid Id) : IQuery;