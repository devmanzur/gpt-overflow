using FluentValidation.Results;

namespace StackOverflow.GPT.Core.Shared.Exceptions;

public class DomainValidationException : BusinessRuleViolationException
{
    public List<ValidationFailure>? Errors { get; }

    public DomainValidationException(string userFriendlyMessage, string systemError, List<ValidationFailure> errors) : base(
        userFriendlyMessage, systemError)
    {
        Errors = errors;
    }
}