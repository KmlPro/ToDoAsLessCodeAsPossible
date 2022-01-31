namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery: IQuery where TResult: IQueryResult
{
    Task<TResult> Handle(TQuery query);
}