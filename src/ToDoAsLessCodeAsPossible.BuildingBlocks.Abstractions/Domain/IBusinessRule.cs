namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Abstractions.Domain;

public interface IBusinessRule
{
    /// <summary>
    ///     The method should return a business exception if validation fails
    /// </summary>
    void CheckIsBroken();
}