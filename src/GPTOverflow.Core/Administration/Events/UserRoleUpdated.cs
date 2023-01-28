using GPTOverflow.Core.CrossCuttinConcerns.Contracts;

namespace GPTOverflow.Core.Administration.Events;

public static class UserRoleUpdated
{
    public record Event() : IDomainEvent;
}