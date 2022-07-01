using NUnit.Framework;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Api.GetEndpoints.Filters.Validator.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Tests.GetEndpoints.Filters.Validator;

public class FilterValidatorTests
{
    [Test]
    public void FieldIsNoFilterable_ThrowsException()
    {
        //Arrange
        var filterDefinition = new FilterDefinition(new List<FilterDefinitionField>()
        {
            new("title", new List<string>() { "eq" })
        });

        var queryFilter = new QueryFilter(new List<QueryFilterField>()
        {
            new("name", "eq", "value")
        });
        
        //Act & Assert
        Assert.Throws<FieldIsNoFilterableException>(() =>
        {
            FilterValidator.ValidateAndThrow(filterDefinition, queryFilter);
        });
    }
    
    [Test]
    public void FieldIsNoFilterableNotSupportedCondition_ThrowsException()
    {
        //Arrange
        var filterDefinition = new FilterDefinition(new List<FilterDefinitionField>()
        {
            new("title", new List<string>() { "eq" })
        });

        var queryFilter = new QueryFilter(new List<QueryFilterField>()
        {
            new("title", "geq", "value")
        });
        
        //Act & Assert
        Assert.Throws<NotSupportedConditionException>(() =>
        {
            FilterValidator.ValidateAndThrow(filterDefinition, queryFilter);
        });
    }
}