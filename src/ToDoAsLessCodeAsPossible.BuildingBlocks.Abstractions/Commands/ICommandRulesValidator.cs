namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandRulesValidator<in TCommand> where TCommand : ICommand
{
    Task<List<string>> ValidateUseCaseRules(TCommand command, CancellationToken cancellationToken);
}
