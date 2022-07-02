using FluentAssertions;
using NUnit.Framework;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Tests.GetEndpoints;

public class FilterParserTests
{
    [Test]
    public void ParseStringAndBooleanValue()
    {
        //Arrange
        var filterQuery = "title[eq]=IamToDo&isCompleted[eq]=true";
        
        //Act 
        var filters = FilterParser.Parse(filterQuery);
        
        //Assert
        filters.Filters.Should().HaveCount(2);
        filters.Filters.First().FieldName.Should().Be("title");
        filters.Filters.First().FilterValue.Should().Be("IamToDo");
        filters.Filters.First().Condition.Should().Be("eq");

        filters.Filters.Last().FieldName.Should().Be("isCompleted");
        filters.Filters.Last().FilterValue.Should().Be("true");
        filters.Filters.Last().Condition.Should().Be("eq");
    } 
}