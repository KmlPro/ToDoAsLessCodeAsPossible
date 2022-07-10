namespace ToDoAsLessCodeAsPossible.Api.UseCases.ChangeToDoTitles;

public record ChangeToDoTitleRequest(Guid Id, string Title);