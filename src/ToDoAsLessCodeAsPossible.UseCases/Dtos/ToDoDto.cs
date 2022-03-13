namespace ToDoAsLessCodeAsPossible.UseCases.Dtos;

public record ToDoDto
{
    public string Id { get; init; }
    public string Title { get; init; }
    public bool IsCompleted { get; init; }
}