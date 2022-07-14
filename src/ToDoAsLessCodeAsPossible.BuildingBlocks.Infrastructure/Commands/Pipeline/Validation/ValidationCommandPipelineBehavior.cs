using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline.Validation;

public class ValidationCommandPipelineBehavior: ICommandPipelineBehavior
{
    private readonly IServiceScopeFactory _serviceFactory;

    public ValidationCommandPipelineBehavior(IServiceScopeFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }
    
    public async Task<CommandResult> HandleAsync<TCommand>(TCommand command, CancellationToken cancellationToken, CommandHandlerDelegate next) where TCommand : ICommand
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
        
        return await next().ConfigureAwait(false);
    }

    private List<string> ValidateStruct<TCommand>(IServiceProvider provider, TCommand command) where TCommand : ICommand
    {
        var structValidator = provider.GetService<ICommandStructValidator<TCommand>>();
        return structValidator != null ? structValidator.ValidateStruct(command) : new List<string>();
    }
    
    private async Task<List<string>> ValidateUseCaseRules<TCommand>(IServiceProvider provider, TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
    {
        var structValidator = provider.GetService<ICommandRulesValidator<TCommand>>();
        return structValidator != null ? await structValidator.ValidateUseCaseRules(command,cancellationToken) : new List<string>();
    }
}