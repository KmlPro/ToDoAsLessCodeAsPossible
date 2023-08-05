using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

internal sealed class UnitOfWorkCommandPipelineBehavior<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult> where TResult: CommandResult
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommandHandler<TCommand, TResult> _commandHandler;
    
    public UnitOfWorkCommandPipelineBehavior(IUnitOfWork unitOfWork, ICommandHandler<TCommand, TResult> commandHandler)
    {
        _unitOfWork = unitOfWork;
        _commandHandler = commandHandler;
    }

    //about ConfigureAwait(false): https://devblogs.microsoft.com/dotnet/configureawait-faq/
    //the reason why i use it here is to improve performance, because there i don't care about synchronization context (like HttpContext.Current)
    public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        await using var transaction = _unitOfWork.BeginTransaction();
        var result = await _commandHandler.HandleAsync(command,cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return result;
    }
}