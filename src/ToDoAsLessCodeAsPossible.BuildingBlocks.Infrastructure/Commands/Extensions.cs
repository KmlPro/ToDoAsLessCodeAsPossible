using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands.Pipeline.Validation;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Commands;

public static class Extensions
{
    /// <summary>
    /// Add required instances for Command Handling 
    /// </summary>
    public static IServiceCollection AddCommands(this IServiceCollection services,
        Assembly assembly)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<ICommandPipelineBehavior,UnitOfWorkCommandPipelineBehavior>();
        services.AddScoped<ICommandPipelineBehavior,ValidationCommandPipelineBehavior>();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes => 
                classes.AssignableTo(typeof(ICommandHandler<>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes => 
                classes.AssignableTo(typeof(ICommandRulesValidator<>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes => 
                classes.AssignableTo(typeof(ICommandStructValidator<>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}