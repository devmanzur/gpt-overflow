using FluentValidation;
using GPTOverflow.Core.CrossCuttingConcerns.Exceptions;

namespace GPTOverflow.Core.CrossCuttingConcerns.Utils;

public static class RuleValidator
{
    public static void Validate<T, TV>(T instance) where T : class where TV : IValidator<T>, new()
    {
        var validator = new TV();
        var validation = validator.Validate(instance);
        if (!validation.IsValid)
        {
            throw new DomainValidationException("One or more validation checks failed",
                "Validation rules check failed", validation.Errors);
        }
    }
    public static async Task ValidateAsync<T, TV>(T request,TV validator) where T : class where TV : IValidator<T>
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
        {
            throw new DomainValidationException("One or more validation checks failed",
                "Validation rules check failed", validation.Errors);
        }
    }
}