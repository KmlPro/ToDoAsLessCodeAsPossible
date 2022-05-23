using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Pipeline.Validation;

internal static class ValidatorGetter
{
    public static dynamic? GetStructValidator(IServiceProvider provider,Type queryType, Type resultType, string typeName)
    {
        var type = typeof(IQueryStructValidator<,>);

        var validator = GetValidator(provider, queryType, resultType, typeName, type);

        return validator;
    }
    
    public static dynamic? GetUseCaseRulesValidator(IServiceProvider provider,Type queryType, Type resultType, string typeName)
    {
        var type = typeof(IQueryRulesValidator<,>);

        var validator = GetValidator(provider, queryType, resultType, typeName, type);

        return validator;
    }

    private static dynamic? GetValidator(IServiceProvider provider, Type queryType, Type resultType, string typeName,
        Type type)
    {
        var queryAndResultTypes = new[] { queryType, resultType };
        var validatorType = type.MakeGenericType(queryAndResultTypes);

        if (validatorType == null)
        {
            throw new UnableToConstructGenericTypeException(typeName);
        }

        dynamic? validator = provider.GetService(validatorType);
        return validator;
    }
}