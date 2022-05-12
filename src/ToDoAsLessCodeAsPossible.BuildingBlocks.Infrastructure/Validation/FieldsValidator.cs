using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MiniValidation;
using ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Validation.Exceptions;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure.Validation;

public class FieldsValidator
{
    internal void ValidateAndThrow<TRequest>(TRequest request)
    {
        if (request == null) return;
        
        var validationResults = new List<ValidationResult>();
        var isValid = TryValidateObject(request, validationResults);

        if (isValid) return;
        
        var errorMessages = validationResults.Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
            .Select(x => x.ErrorMessage!).ToList();
            
        throw new InvalidParametersException(errorMessages);
    }

    private static bool TryValidateObject<TRequest>([DisallowNull] TRequest request, List<ValidationResult> validationResults)
    {
        return MiniValidator.TryValidate(request, out var errors);
    }
}