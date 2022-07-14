using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

internal sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;

    public CommandDispatcher(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    //Credits to: https://github.com/jbogard/MediatR/blob/8492e9e050e87c4e6e9837bd3af8cd6506aaa4af/src/MediatR/Wrappers/RequestHandlerWrapper.cs
    public async Task<CommandResult> SendAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : class, ICommand
    {
        using var scope = _serviceFactory.CreateScope();
        var provider = scope.ServiceProvider;
        
        var handler = provider.GetService<ICommandHandler<TCommand>>();
        if (handler == null)
        {
            throw new CommandHandlerNotFoundException(typeof(TCommand).Name);
        }

        Task<CommandResult> Handler() => handler.HandleAsync(command, token);

        return await provider
            .GetServices<ICommandPipelineBehavior>()
            .Reverse()
            .Aggregate((CommandHandlerDelegate) Handler, (next, pipeline) => async () => await pipeline.HandleAsync(command, token, next))();
    }
}