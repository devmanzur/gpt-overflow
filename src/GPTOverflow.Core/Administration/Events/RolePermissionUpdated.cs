using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.Administration.Events;

public static class RolePermissionUpdated
{
    public record Event() : IDomainEvent;

}