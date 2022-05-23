namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IQueryStructValidator<in TQuery, TResult> where TQuery: IQuery<TResult>
{
    List<string> ValidateStruct(TQuery query);
}