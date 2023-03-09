using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using GPTOverflow.Core.UserManagement.Events;

namespace GPTOverflow.Core.UserManagement.Models;

public enum UserStatus
{
    Active,
    Banned
}

public class ApplicationUser : AggregateRoot, IAuditable
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }

    protected ApplicationUser()
    {
        
    }
    public ApplicationUser(string emailAddress)
    {
        EmailAddress = emailAddress;
        Username = DomainUtils.CreateUsername(emailAddress);
        Name = DomainUtils.CreateUsername(emailAddress);
        Status = UserStatus.Active;
        AddDomainEvent(new UserCreated.Event(this));
    }

    public string Username { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public UserStatus Status { get; set; }
    public DateTime? SuspendedUntil { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public bool IsSuspended()
    {
        return SuspendedUntil != null && SuspendedUntil > DateTime.UtcNow;
    }
}