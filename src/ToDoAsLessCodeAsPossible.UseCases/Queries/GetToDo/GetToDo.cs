using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

namespace ToDoAsLessCodeAsPossible.UseCases.Queries.GetToDo;

public record GetToDo(string Id) : IQuery;