namespace GPTOverflow.Core.CrossCuttingConcerns.Exceptions;

public static class DefaultErrorMessages
{
    public const string ServerError = "Sorry, Something wrong on our side, Please try again";

    public const string ConstraintViolation =
        "Requested action cannot be allowed as it violates critical system invariants";
}