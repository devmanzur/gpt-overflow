using GPTOverflow.Core.Shared.Contracts;

namespace GPTOverflow.Core.Administration.Events;

public static class UserRoleUpdated
{
    public record Event() : IDomainEvent;
}