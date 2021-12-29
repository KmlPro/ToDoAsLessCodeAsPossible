using ToDoAsLessCodeAsPossible.BuildingBlocks.UseCases.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

internal sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public CommandDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task SendAsync<T>(T command, CancellationToken token) where T : class, ICommand
    {
        using var scope = _serviceFactory.CreateScope();

        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>();
        await handler.HandleAsync(command, token);
    }
}