namespace GPTOverflow.Core.CrossCuttingConcerns.Exceptions;

/// <summary>
/// This is used as the go to exception for any system constraint violation
/// Any business rule violation exception thrown by the business rule validators should use this directly
/// or extend this one
/// </summary>
public class BusinessRuleViolationException : BaseApplicationException
{
    public BusinessRuleViolationException(string userFriendlyMessage, string? systemError = null) : base(
        userFriendlyMessage, new Exception(systemError ?? userFriendlyMessage), 400)
    {
    }
}