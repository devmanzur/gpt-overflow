using GPTOverflow.Core.CrossCuttingConcerns.Contracts;

namespace GPTOverflow.Core.UserManagement.Models;

public class RoleAccessPermission : IEntity, IAuditable
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public Guid AccessPermissionId { get; set; }
    public AccessPermission AccessPermission { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
    public Guid Id { get; set; }
}