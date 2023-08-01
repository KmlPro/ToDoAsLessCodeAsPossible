namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandStructValidator<in TCommand, TResult>
    where TCommand : ICommand<TResult> where TResult : CommandResult
{
    List<string> ValidateStruct(TCommand query);
}