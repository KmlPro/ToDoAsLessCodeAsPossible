using System.ComponentModel.DataAnnotations;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Api.Requests.Validation;

internal class RequestValidator
{
    internal void ValidateAndThrow<TRequest>(TRequest request)
    {
        if (request == null) return;
        
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true);

        if (isValid) return;
        
        var errorMessages = validationResults.Where(x => !string.IsNullOrEmpty(x.ErrorMessage))
            .Select(x => x.ErrorMessage!).ToList();
            
        throw new RequestValidationException(errorMessages);
    }
}