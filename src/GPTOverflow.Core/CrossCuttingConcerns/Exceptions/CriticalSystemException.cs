namespace GPTOverflow.Core.CrossCuttingConcerns.Exceptions;

public class CriticalSystemException : BaseApplicationException
{
    public CriticalSystemException(string systemError,string userFriendlyMessage="An unexpected error occurred, please try again later") : base(
        userFriendlyMessage,  new Exception(systemError))
    {
    }
}