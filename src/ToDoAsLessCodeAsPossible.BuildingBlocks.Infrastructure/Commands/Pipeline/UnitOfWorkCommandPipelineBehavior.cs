using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Transactions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

internal sealed class UnitOfWorkCommandPipelineBehavior : ICommandPipelineBehavior
{
    private IUnitOfWork unitOfWork;
    
    public UnitOfWorkCommandPipelineBehavior(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    //about ConfigureAwait(false): https://devblogs.microsoft.com/dotnet/configureawait-faq/
    //the reason why i use it here is to improve performance, because there i don't care about synchronization context (like HttpContext.Current)
    public async Task<CommandResult> HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
    {
        await using var transaction = unitOfWork.BeginTransaction();
        var result = await next().ConfigureAwait(false);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return result;
    }
}