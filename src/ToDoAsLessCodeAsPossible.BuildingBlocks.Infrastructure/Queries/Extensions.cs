using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Pipelines;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Pipeline.Validation;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries;

public static class Extensions
{
    /// <summary>
    /// Add required instances for Queries Handling
    /// </summary>
    public static IServiceCollection AddQueries(this IServiceCollection services,
        Assembly assembly)
    {
        var sharedLibraryAssembly = typeof(Extensions).Assembly;

        services
            .AddPipeline()
            .AddInput(typeof(IQuery<>))
            .AddHandler(typeof(IQueryHandler<,>), assembly)
            .AddDispatcher<IQueryDispatcher>(new DispatcherOptions(true),sharedLibraryAssembly)
            .WithOpenTypeDecorator(typeof(ValidationQueryPipelineBehavior<,>))
            .Build();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes =>
                classes.AssignableTo(typeof(IQueryRulesValidator<,>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(classes =>
                classes.AssignableTo(typeof(IQueryStructValidator<,>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}