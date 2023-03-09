using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.UserManagement.Events;

public static class UserRoleUpdated
{
    public record Event() : IDomainEvent;
}