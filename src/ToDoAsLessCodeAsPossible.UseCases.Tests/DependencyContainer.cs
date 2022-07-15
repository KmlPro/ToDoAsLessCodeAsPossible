using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.InMemory;
using ToDoAsLessCodeAsPossible.Infrastructure;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence;

namespace ToDoAsLessCodeAsPossible.UseCases.Tests;

public class DependencyContainer
{
    private IServiceProvider _provider;
    private readonly IServiceCollection _services;
    
    public DependencyContainer()
    {
        _services = new ServiceCollection();
        _services.AddInfrastructure();
        _services.AddInMemoryDatabase<ToDoWriteDbContext>(new InMemoryDatabaseParameters(InMemoryDatabaseProvider.Sqlite));
    }

    public void BuildContainerAndSetupDatabase()
    {
        _provider = _services.BuildServiceProvider();
        
        DatabaseCreator.CreateDatabaseSchema(_provider);
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