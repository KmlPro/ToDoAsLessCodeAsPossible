using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pipelines;
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
        var sharedLibraryAssembly = typeof(Extensions).Assembly;

        services
            .AddPipeline()
            .AddInput(typeof(ICommand<>))
            .AddHandler(typeof(ICommandHandler<,>), assembly)
            .AddDispatcher<ICommandDispatcher>(new DispatcherOptions(false), sharedLibraryAssembly)
            .WithOpenTypeDecorator(typeof(ValidationCommandPipelineBehavior<,>))
            .WithOpenTypeDecorator(typeof(UnitOfWorkCommandPipelineBehavior<,>))
            .Build();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes =>
                classes.AssignableTo(typeof(ICommandRulesValidator<,>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes =>
                classes.AssignableTo(typeof(ICommandStructValidator<,>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}