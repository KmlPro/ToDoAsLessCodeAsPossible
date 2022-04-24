using System.ComponentModel.DataAnnotations;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.CreateToDos;

public record CreateToDoRequest
{
    // kbytner 18.04.2022 - to do move validation to use case
    [MinLength(2, ErrorMessage = "Title should have at least 2 characters")]
    public string Title { get; set; }
}