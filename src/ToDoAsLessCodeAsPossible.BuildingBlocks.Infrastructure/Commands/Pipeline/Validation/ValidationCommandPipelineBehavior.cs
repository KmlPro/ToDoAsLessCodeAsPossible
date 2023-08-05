using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline.Validation;

public class ValidationCommandPipelineBehavior<TCommand, TResult> : ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult> where TResult: CommandResult
{
    private readonly IServiceScopeFactory _serviceFactory;
    private readonly ICommandHandler<TCommand, TResult> _commandHandler;

    public ValidationCommandPipelineBehavior(IServiceScopeFactory serviceFactory, ICommandHandler<TCommand, TResult> commandHandler)
    {
        _serviceFactory = serviceFactory;
        _commandHandler = commandHandler;
    }
    
    public async Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken)
    {
        using var scope = _serviceFactory.CreateScope();
        var provider = scope.ServiceProvider;

        var structValidationResult = ValidateStruct(provider,command);
        if (structValidationResult.Any())
        {
            throw new InvalidCommandStructException(structValidationResult);
        }
        
        var useCaseRulesValidationResult = await ValidateUseCaseRules(provider,command, cancellationToken);
        if (useCaseRulesValidationResult.Any())
        {
            throw new CommandRulesBrokenException(useCaseRulesValidationResult);
        }
        
        return await _commandHandler.HandleAsync(command, cancellationToken).ConfigureAwait(false);
    }

    private List<string> ValidateStruct(IServiceProvider provider, TCommand command)
    {
        var structValidator = provider.GetService<ICommandStructValidator<TCommand,TResult>>();
        return structValidator != null ? structValidator.ValidateStruct(command) : new List<string>();
    }
    
    private async Task<List<string>> ValidateUseCaseRules(IServiceProvider provider, TCommand command, CancellationToken cancellationToken)
    {
        var structValidator = provider.GetService<ICommandRulesValidator<TCommand,TResult>>();
        return structValidator != null ? await structValidator.ValidateUseCaseRules(command,cancellationToken) : new List<string>();
    }
}