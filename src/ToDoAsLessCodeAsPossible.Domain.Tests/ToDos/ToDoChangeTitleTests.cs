using NUnit.Framework;
using Shouldly;
using ToDoAsLessCodeAsPossible.Domain.ToDos;

namespace ToDoAsLessCodeAsPossible.Domain.Tests.ToDos;

public class ToDoChangeTitleTests
{
    [Test]
    public void TitleIsNull_ThrowsArgumentNullException()
    {
        //Arrange
        var toDo = ToDo.Create("MyToDo");
        
        //Act & Asser
        Should.Throw<ArgumentNullException>(() => toDo.ChangeTitle(null));
    }
    
    [Test]
    public void TitleIsNull_ThrowsExceptionCompleted()
    {
        //Arrange
        var toDo = ToDo.Create("MyToDo");
        
        //Act & Asser
        Should.Throw<ArgumentException>(() => toDo.ChangeTitle(""));
    }
}