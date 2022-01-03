using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

internal class RequestValidator
{
    internal void ValidateAndThrow<TRequest>(TRequest request)
    {
        if (request == null) return;
        
        var validationResults = new List<ValidationResult>();
        var isValid = TryValidateObject(request, validationResults);

        if (isValid) return;
        
        var errorMessages = validationResults.Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
            .Select(x => x.ErrorMessage!).ToList();
            
        throw new InvalidRequestException(errorMessages);
    }

    private static bool TryValidateObject<TRequest>([DisallowNull] TRequest request, List<ValidationResult> validationResults)
    {
        return Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);
    }
}