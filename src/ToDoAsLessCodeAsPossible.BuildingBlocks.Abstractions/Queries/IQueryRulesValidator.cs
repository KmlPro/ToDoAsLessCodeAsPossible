namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IQueryRulesValidator<in TQuery, TResult> where TQuery: IQuery<TResult>
{
    Task<List<string>> ValidateUseCaseRules(TQuery query, CancellationToken cancellationToken);
}
