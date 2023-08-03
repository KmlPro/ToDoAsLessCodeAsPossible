using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Queries.Pipeline.Validation;

public class ValidationQueryPipelineBehavior<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    private const string QUERY_VALIDATE_STRUCT_METHOD_NAME = "ValidateStruct";
    private const string QUERY_VALIDATE_USECASE_RULES_METHOD_NAME = "ValidateUseCaseRules";

    private readonly IServiceScopeFactory _serviceFactory;
    private readonly IQueryHandler<TQuery, TResult> _queryHandler;

    public ValidationQueryPipelineBehavior(IServiceScopeFactory serviceFactory,
        IQueryHandler<TQuery, TResult> queryHandler)
    {
        _serviceFactory = serviceFactory;
        _queryHandler = queryHandler;
    }

    public async Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken)
    {
        using var scope = _serviceFactory.CreateScope();
        var provider = scope.ServiceProvider;

        var queryType = query.GetType();
        var queryName = queryType.Name;

        var structValidationResult = ValidateStruct(provider, queryType, typeof(TResult), queryName, query);

        if (structValidationResult.Any())
        {
            throw new InvalidQueryStructException(structValidationResult);
        }

        var useCaseRulesValidationResult =
            await ValidateUseCaseRules(provider, queryType, typeof(TResult), queryName, query, cancellationToken);

        if (useCaseRulesValidationResult.Any())
        {
            throw new QueryRulesBrokenException(useCaseRulesValidationResult);
        }

        return await _queryHandler.HandleAsync(query, cancellationToken).ConfigureAwait(false);
    }

    private List<string> ValidateStruct<TQuery>(IServiceProvider provider, Type queryType, Type resultType,
        string queryName, TQuery query)
    {
        var validator = ValidatorGetter.GetStructValidator(provider, queryType, resultType, queryName);
        if (validator == null)
        {
            return new List<string>();
        }

        MethodInfo methodInfo = MethodInfoGetter.GetByName(validator, QUERY_VALIDATE_STRUCT_METHOD_NAME);

        if (methodInfo == null)
        {
            throw new MethodNotFoundInGenericTypeException(queryName, QUERY_VALIDATE_STRUCT_METHOD_NAME);
        }

        return MethodExecutor.Invoke<List<string>>(methodInfo, validator, (dynamic)query);
    }

    private async Task<List<string>> ValidateUseCaseRules<TQuery>(IServiceProvider provider, Type queryType,
        Type resultType, string queryName, TQuery query, CancellationToken cancellationToken)
    {
        var validator = ValidatorGetter.GetUseCaseRulesValidator(provider, queryType, resultType, queryName);
        if (validator == null)
        {
            return new List<string>();
        }

        MethodInfo methodInfo = MethodInfoGetter.GetByName(validator, QUERY_VALIDATE_USECASE_RULES_METHOD_NAME);

        if (methodInfo == null)
        {
            throw new MethodNotFoundInGenericTypeException(queryName, QUERY_VALIDATE_USECASE_RULES_METHOD_NAME);
        }


        List<string> result =
            await MethodExecutor.InvokeAsync<List<string>>(methodInfo, validator, (dynamic)query, cancellationToken);

        return result;
    }
}