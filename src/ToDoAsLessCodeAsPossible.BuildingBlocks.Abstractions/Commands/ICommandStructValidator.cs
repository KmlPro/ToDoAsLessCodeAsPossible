namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

public interface ICommandStructValidator<in TCommand> where TCommand : ICommand
{
    List<string> ValidateStruct(TCommand query);
}