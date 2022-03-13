using System.ComponentModel.DataAnnotations;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;

public record CreateToDoRequest
{
    [MinLength(2, ErrorMessage = "Title should have at least 2 characters")]
    public string Title { get; set; }
}