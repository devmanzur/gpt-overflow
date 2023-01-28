using StackOverflow.GPT.Core.Shared.Contracts;

namespace StackOverflow.GPT.Core.Administration.Models;

public enum UserStatus
{
    Active,
    Banned
}

public class ApplicationUser : IEntity, IAuditable
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }

    public ApplicationUser(string emailAddress, string name)
    {
        EmailAddress = emailAddress;
        Name = name;
        Status = UserStatus.Active;
    }
    
    private readonly List<ApplicationUserRole> _roles = new List<ApplicationUserRole>();
    public IReadOnlyList<ApplicationUserRole> Roles => _roles.AsReadOnly();

    public string Name { get; private set; }
    public string EmailAddress { get; private set; }
    public UserStatus Status { get; private set; }
    public DateTime? SuspendedUntil { get; private set; }

    public void SetName(string name)
    {
        //TODO: Only a user can update his own name
        this.Name = name;
    }

    public void Ban()
    {
        this.Status = UserStatus.Banned;
    }

    public void Suspend()
    {
        this.SuspendedUntil = DateTime.UtcNow.AddMonths(2);
    }

    public bool IsAllowedToSignIn()
    {
        return Status == UserStatus.Active && !IsSuspended();
    }

    private bool IsSuspended()
    {
        return SuspendedUntil != null && SuspendedUntil > DateTime.UtcNow;
    }

}