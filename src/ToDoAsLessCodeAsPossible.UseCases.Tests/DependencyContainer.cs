using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.Infrastructure;

namespace ToDoAsLessCodeAsPossible.UseCases.Tests;

public class DependencyContainer
{
    private IServiceProvider _provider;
    private readonly IServiceCollection _services;
    
    public DependencyContainer()
    {
        _services = new ServiceCollection();
        _services.AddInfrastructure();
        
        //TO DO Add configuration for in memory database
    }

    public void BuildContainer()
    {
        _provider = _services.BuildServiceProvider();
    }

    public void RegisterMockType<TType>(object instance) where TType: class
    {
        _services.AddScoped<TType>(x => (TType)instance);
    }

    public TService GetService<TService>() where TService : notnull
    {
        return _provider.GetRequiredService<TService>();
    }

    public ICommandDispatcher GetCommandDispatcher()
    {
        return _provider.GetRequiredService<ICommandDispatcher>();
    }
    
    public IQueryDispatcher GetQueryDispatcher()
    {
        return _provider.GetRequiredService<IQueryDispatcher>();
    }
}