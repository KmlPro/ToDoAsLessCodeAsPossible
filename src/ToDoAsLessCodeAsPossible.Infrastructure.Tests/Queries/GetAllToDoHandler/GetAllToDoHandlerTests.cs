using NUnit.Framework;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.Infrastructure.Persistence;
using ToDoAsLessCodeAsPossible.UseCases.Queries.GetAllToDo;

namespace ToDoAsLessCodeAsPossible.Infrastructure.Tests.Queries.GetAllToDoHandler;

public class GetAllToDoHandlerTests
{
    private TestFixture _testFixture;
    
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly DependencyContainer _dependencyContainer;
    
    public GetAllToDoHandlerTests()
    {
        _dependencyContainer = new DependencyContainer();
        _dependencyContainer.BuildContainerAndSetupDatabase();
        _queryDispatcher = _dependencyContainer.GetQueryDispatcher();
        var dbContext = _dependencyContainer.GetService<ToDoWriteDbContext>();
        _testFixture = new TestFixture(dbContext);
    }
    
    [Test]
    public async Task EmptyGetAllToDoQuery_ResultsReturned()
    {
        //Arrange
        var toDoCount = 3;
        _testFixture.CreateAndSaveInDbRandomToDos(toDoCount);
        var query = new GetAllToDo(new QueryFilter(new List<QueryFilterField>()));
        
        //Act
        var result = await _queryDispatcher.HandleAsync(query, CancellationToken.None);
        
        //Assert
        Assert.AreEqual(result.Count(), toDoCount);
    }
}