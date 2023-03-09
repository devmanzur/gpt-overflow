using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.UserManagement.Events;

namespace GPTOverflow.Core.UserManagement.Models;
public class Role : AggregateRoot
{
    protected Role()
    {
        
    }
    
    public Role(UserRole name)
    {
        Name = name;
    }
    public UserRole Name { get; set; }

    private readonly List<RoleAccessPermission> _permissions = new List<RoleAccessPermission>();
    public IReadOnlyList<RoleAccessPermission> Permissions => _permissions.AsReadOnly();

    public void AddPermission(AccessPermission permission)
    {
        AddDomainEvent(new RolePermissionUpdated.Event());
        _permissions.Add(new RoleAccessPermission()
        {
            RoleId = Id,
            AccessPermissionId = permission.Id
        });
    }
}

public enum UserRole
{
    Member,
    Moderator
}