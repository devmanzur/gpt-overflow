using StackOverflow.GPT.Core.Shared.Contracts;

namespace StackOverflow.GPT.Core.Administration.Events;

public static class RolePermissionUpdated
{
    public record Event() : IDomainEvent;

}