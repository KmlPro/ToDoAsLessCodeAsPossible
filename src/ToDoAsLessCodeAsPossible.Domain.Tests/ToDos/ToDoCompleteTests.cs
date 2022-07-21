using NUnit.Framework;
using Shouldly;
using ToDoAsLessCodeAsPossible.Domain.ToDos;
using ToDoAsLessCodeAsPossible.Domain.ToDos.Rules.CantCompleteAlreadyCompletedToDo;

namespace ToDoAsLessCodeAsPossible.Domain.Tests.ToDos;

public class ToDoCompleteTests
{
    [Test]
    public void ToDoUncompleted_SuccessfullyCompleted()
    {
        //Arrange
        var toDo = ToDo.Create("MyToDo");
        
        //Act
        toDo.Complete();
        
        //Assert
        toDo.IsCompleted.ShouldBe(true);
    }
    
    [Test]
    public void ToDoCompleted_ThrowsException()
    {
        //Arrange
        var toDo = ToDo.Create("MyToDo");
        toDo.Complete();
        
        //Act & Asser
        Should.Throw<CantCompleteAlreadyCompletedToDoException>(() => toDo.Complete());
    }
}