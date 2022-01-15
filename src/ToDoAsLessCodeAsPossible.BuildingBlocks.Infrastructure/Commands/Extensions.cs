using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

public static class Extensions
{
    /// <summary>
    /// Add required instances for Command Handling 
    /// </summary>
    public static IServiceCollection AddCommands(this IServiceCollection services,
        Assembly assembly)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<TransactionScopeFactory>();
        services.AddSingleton<ICommandPipelineBehavior,UnitOfWorkCommandPipelineBehavior>();
        services.AddSingleton<ICommandPipelineBehavior,ValidationCommandPipelineBehavior>();
        
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes => 
                classes.AssignableTo(typeof(ICommandHandler<>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}