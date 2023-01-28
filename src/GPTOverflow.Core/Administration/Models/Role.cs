using GPTOverflow.Core.Administration.Events;
using GPTOverflow.Core.CrossCuttinConcerns.Contracts;

namespace GPTOverflow.Core.Administration.Models;
public class Role : AggregateRoot
{
    public RoleType Type { get; set; }

    private readonly List<RoleAccessPermission> _permissions = new List<RoleAccessPermission>();
    public IReadOnlyList<RoleAccessPermission> Permissions => _permissions.AsReadOnly();

    public void GrantAccess(AccessPermission permission)
    {
        AddDomainEvent(new RolePermissionUpdated.Event());
        _permissions.Add(new RoleAccessPermission()
        {
            RoleId = Id,
            AccessPermissionId = permission.Id
        });
    }
}

public enum RoleType
{
    Member,
    Moderator,
    Admin,
}