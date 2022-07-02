using NUnit.Framework;
using Shouldly;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries.Filters.Exceptions;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Persistance.Sql.Filter;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Tests.Persistance.Sql.Filter;

public class SqlQueryBuilderTests
{
    [Test]
    public void GenerateQuery_ConditionMappingExists_FieldMappingExists_ReturnSqlQuery()
    {
        //Arrange
        var builder = new SqlLiteQueryBuilder();
        var baseQuery = "select Id, Title, IsCompleted from ToDo";
        var queryFilter = new QueryFilter(new List<QueryFilterField>()
            { new("title", "eq", "IamToDo") });
        
        builder.WithBaseQuery(baseQuery);
        builder.WithFieldMapping("title", "Title");
        builder.WithQueryFilter(queryFilter);
        
        //Act
        var sql = builder.Build();
        
        //Assert
        sql.ShouldBe(baseQuery + " WHERE Title = IamToDo");
    }
    
    [Test]
    public void GenerateQuery_FieldMappingDoesNotExists_ThrowsException()
    {
        //Arrange
        var builder = new SqlLiteQueryBuilder();
        var baseQuery = "select Id, Title, IsCompleted from ToDo";
        var queryFilter = new QueryFilter(new List<QueryFilterField>()
            { new("title", "eq", "IamToDo") });
        
        builder.WithBaseQuery(baseQuery);
        builder.WithQueryFilter(queryFilter);
        
        //Act & Assert
        Should.Throw<MappingForFieldNotFoundException>(() => builder.Build());
    }
    
    [Test]
    public void GenerateQuery_ConditionMappingDoesNotExists_ThrowsException()
    {
        //Arrange
        var builder = new SqlLiteQueryBuilder();
        var baseQuery = "select Id, Title, IsCompleted from ToDo";
        var queryFilter = new QueryFilter(new List<QueryFilterField>()
            { new("title", "geq", "IamToDo") });
        
        builder.WithBaseQuery(baseQuery);
        builder.WithFieldMapping("title", "Title");
        builder.WithQueryFilter(queryFilter);
        
        //Act & Assert
        Should.Throw<MappingForConditionNotFoundException>(() => builder.Build());
    }
}