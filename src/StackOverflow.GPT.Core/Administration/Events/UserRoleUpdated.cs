using StackOverflow.GPT.Core.Shared.Contracts;

namespace StackOverflow.GPT.Core.Administration.Events;

public static class UserRoleUpdated
{
    public record Event() : IDomainEvent;
}