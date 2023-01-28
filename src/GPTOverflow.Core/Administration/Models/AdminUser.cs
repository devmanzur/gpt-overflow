using GPTOverflow.Core.Shared.Contracts;

namespace GPTOverflow.Core.Administration.Models;

public class AdminUser: IEntity, IAuditable
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}