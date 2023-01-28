using GPTOverflow.Core.CrossCuttinConcerns.Contracts;

namespace GPTOverflow.Core.Administration.Events;

public static class RolePermissionUpdated
{
    public record Event() : IDomainEvent;

}