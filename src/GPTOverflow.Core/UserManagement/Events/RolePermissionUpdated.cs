using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.UserManagement.Events;

public static class RolePermissionUpdated
{
    public record Event() : IDomainEvent;

}