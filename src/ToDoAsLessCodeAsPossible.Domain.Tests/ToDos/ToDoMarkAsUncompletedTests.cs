using NUnit.Framework;
using Shouldly;
using ToDoAsLessCodeAsPossible.Domain.ToDos;
using ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantMarkAsUncompletedAlreadyUncompleted;

namespace ToDoAsLessCodeAsPossible.Domain.Tests.ToDos;

public class ToDoMarkAsUncompletedTests
{
    [Test]
    public void ToDoCompleted_SuccessfullyCompleted()
    {
        //Arrange
        var toDo = ToDo.Create("MyToDo");
        toDo.Complete();

        //Act
        toDo.MarkAsUnCompleted();
        
        //Assert
        toDo.IsCompleted.ShouldBe(false);
    }
    
    [Test]
    public void ToDoCompleted_ThrowsException()
    {
        //Arrange
        var toDo = ToDo.Create("MyToDo");
        
        //Act & Asser
        Should.Throw<CantMarkAsUncompletedAlreadyUncompletedException>(() => toDo.MarkAsUnCompleted());
    }
}