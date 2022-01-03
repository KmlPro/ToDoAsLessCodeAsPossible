using ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Commands;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

internal sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public CommandDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task SendAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : class, ICommand
    {
        using var scope = _serviceFactory.CreateScope();

        var handler = scope.ServiceProvider.GetService<ICommandHandler<TCommand>>();
        if (handler == null)
        {
            throw new CommandHandlerNotFoundException(typeof(TCommand).Name);
        }
        await handler.HandleAsync(command, token);
    }
}