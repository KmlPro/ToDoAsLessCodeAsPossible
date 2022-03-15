using System.ComponentModel.DataAnnotations;

namespace ToDoAsLessCodeAsPossible.Api.UseCases.GetToDo;

public record GetToDoRequest
{
    [Required]
    public string Id { get; set; }
}