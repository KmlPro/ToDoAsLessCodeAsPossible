using System.ComponentModel.DataAnnotations;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;

public record CreateToDoRequest(string Title);