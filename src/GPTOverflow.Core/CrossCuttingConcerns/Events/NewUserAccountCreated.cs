using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.CrossCuttingConcerns.Events;

public class NewUserAccountCreated : IDomainEvent
{
    public string Username { get; }

    public NewUserAccountCreated(string username)
    {
        Username = username;
    }
}