using StackOverflow.GPT.Core.Shared.Contracts;
using StackOverflow.GPT.Core.Shared.Utils;

namespace StackOverflow.GPT.Core.Administration.Models;

public enum Permission
{
    UpdateRolePermission
}

public class AccessPermission : IEntity, IAuditable
{
    public AccessPermission(Permission name)
    {
        Name = name;
        DisplayName = name.ToSpacedSentence();
    }

    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }

    public Permission Name { get; set; }
    public string DisplayName { get; set; }
}
