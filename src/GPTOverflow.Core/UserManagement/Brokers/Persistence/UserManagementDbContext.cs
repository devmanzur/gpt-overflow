using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Persistence;
using GPTOverflow.Core.UserManagement.Brokers.Persistence.Configurations;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence;

public class UserManagementDbContext : BaseDbContext<UserManagementDbContext>
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<AdminUser> Admins { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AccessPermission> AccessPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("user-management");
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AccessPermissionConfig());
        builder.ApplyConfiguration(new AdminUserConfig());
        builder.ApplyConfiguration(new ApplicationUserConfig());
        builder.ApplyConfiguration(new RoleConfig());
    }
}