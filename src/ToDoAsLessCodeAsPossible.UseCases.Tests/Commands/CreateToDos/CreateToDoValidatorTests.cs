using Moq;
using NUnit.Framework;
using ToDoAsLessCodeAsPossible.UseCases.Commands.CreateToDos;
using ToDoAsLessCodeAsPossible.UseCases.Services;

namespace ToDoAsLessCodeAsPossible.UseCases.Tests.Commands.CreateToDos;

public class CreateToDoValidatorTests
{
    private readonly IToDoWriteRepository _toDoWriteRepository;

    public CreateToDoValidatorTests()
    {
        _toDoWriteRepository = CreateMockRepository();
    }
    
    [Test]
    public async Task ValidateUseCaseRules_ToDoWithSameNameAlreadyExists_ReturnsError()
    {
        //Arrange 
        var validator = new CreateToDoValidator(_toDoWriteRepository);
        var createToDoCommand = new CreateToDo("Sample Title");
        
        //Act
        var errors = await validator.ValidateUseCaseRules(createToDoCommand, CancellationToken.None);
        
        //Assert
        Assert.AreEqual(1, errors.Count);
        Assert.AreEqual(errors.First(), "Title with exactly same title already exist");
    }

    private IToDoWriteRepository CreateMockRepository()
    {
        var toDoRepositoryMock = new Mock<IToDoWriteRepository>();
        toDoRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        return toDoRepositoryMock.Object;
    }
} 