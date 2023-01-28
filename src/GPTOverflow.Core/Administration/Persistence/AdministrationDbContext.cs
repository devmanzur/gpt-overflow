using GPTOverflow.Core.Administration.Models;
using GPTOverflow.Core.CrossCuttinConcerns.Contracts;
using GPTOverflow.Core.CrossCuttinConcerns.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GPTOverflow.Core.Administration.Persistence;

public class AdministrationDbContext : BaseDbContext<AdministrationDbContext>
{
    public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<AdminUser> Admins { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AccessPermission> AccessPermissions { get; set; }
}