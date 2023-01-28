using GPTOverflow.Core.Shared.Contracts;

namespace GPTOverflow.Core.Administration.Events;

public static class RolePermissionUpdated
{
    public record Event() : IDomainEvent;

}