using StackOverflow.GPT.Core.Administration.Events;
using StackOverflow.GPT.Core.Shared.Contracts;

namespace StackOverflow.GPT.Core.Administration.Models;

public enum UserRole
{
    Member,
    Moderator,
    Admin
}

public class Role : AggregateRoot
{
    public Role(UserRole name)
    {
        Name = name;
    }

    public UserRole Name { get; private set; }

    private readonly List<RoleAccessPermission> _permissions = new List<RoleAccessPermission>();
    public IReadOnlyList<RoleAccessPermission> Permissions => _permissions.AsReadOnly();

    private readonly List<ApplicationUserRole> _users = new List<ApplicationUserRole>();
    public IReadOnlyList<ApplicationUserRole> Users => _users.AsReadOnly();

    public void GrantAccess(AccessPermission permission)
    {
        AddDomainEvent(new RolePermissionUpdated.Event());
        _permissions.Add(new RoleAccessPermission()
        {
            RoleId = Id,
            AccessPermissionId = permission.Id
        });
    }

    public void AddUser(ApplicationUser applicationUser)
    {
        AddDomainEvent(new UserRoleUpdated.Event());
        _users.Add(new ApplicationUserRole()
        {
            ApplicationUserId = applicationUser.Id,
            RoleId = this.Id
        });
    }
}