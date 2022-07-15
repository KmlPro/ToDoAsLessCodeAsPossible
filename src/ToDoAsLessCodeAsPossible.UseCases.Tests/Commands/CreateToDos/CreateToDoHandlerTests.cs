using NUnit.Framework;
using Shouldly;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Commands;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.ValueObjects;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDos;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Tests.Commands.CreateToDos;

public class CreateToDoHandlerTests
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly DependencyContainer _dependencyContainer;
    private readonly IToDoWriteRepository _toDoWriteRepository;
    
    public CreateToDoHandlerTests()
    {
        _dependencyContainer = new DependencyContainer();
        _dependencyContainer.BuildContainerAndSetupDatabase();
        _commandDispatcher = _dependencyContainer.GetCommandDispatcher();
        _toDoWriteRepository = _dependencyContainer.GetService<IToDoWriteRepository>();
    }
    
    [Test]
    public async Task CorrectCommandParameters_ToDoCreated()
    {
        //Arrange
        var command = new CreateToDo("Fancy to Do");
        
        //Act
        var result = await _commandDispatcher.SendAsync(command, CancellationToken.None);
        
        //Assert
        var id = new EntityId(new Guid(result.Id));
        var toDo = await _toDoWriteRepository.GetAsync(id, CancellationToken.None);
        
        result.Id.ShouldNotBeNull();
        toDo.ShouldNotBeNull();
    }
    
}