using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Administration.Events;

public static class UserRoleUpdated
{
    public record Event() : IDomainEvent;
}