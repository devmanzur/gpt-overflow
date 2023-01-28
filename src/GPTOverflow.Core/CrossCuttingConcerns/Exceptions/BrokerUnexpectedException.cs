namespace GPTOverflow.Core.CrossCuttingConcerns.Exceptions;

public class BrokerUnexpectedException : BaseApplicationException
{
    public BrokerUnexpectedException(string brokerName, string errorMessage, string payload) : base("Something went wrong!",
        new Exception($"An unexpected error occurred on {brokerName}. The error Message is {errorMessage}. The request payload was {payload}"))
    {
    }
}