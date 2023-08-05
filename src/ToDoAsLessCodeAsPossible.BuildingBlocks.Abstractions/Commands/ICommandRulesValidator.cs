namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandRulesValidator<in TCommand, TResult>
    where TCommand : ICommand<TResult> where TResult : CommandResult
{
    Task<List<string>> ValidateUseCaseRules(TCommand command, CancellationToken cancellationToken);
}