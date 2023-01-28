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
        Username = CreateUsername(emailAddress);
    }

    /// <summary>
    /// Creates username from email address,
    /// </summary>
    /// <example>
    /// email = manzur123@gmail.com
    /// username = @manzur123
    /// </example>
    /// <param name="email"></param>
    /// <returns></returns>
    private static string CreateUsername(string email)
    {
        return $"@{email.Split("@")[0]}";
    }

    public string Username { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public UserStatus Status { get; set; }
    public DateTime? SuspendedUntil { get; set; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public bool IsAllowedToSignIn()
    {
        return Status == UserStatus.Active && !IsSuspended();
    }

    private bool IsSuspended()
    {
        return SuspendedUntil != null && SuspendedUntil > DateTime.UtcNow;
    }
}