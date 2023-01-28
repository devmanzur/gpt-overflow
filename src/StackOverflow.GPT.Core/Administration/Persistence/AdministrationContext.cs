using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StackOverflow.GPT.Core.Administration.Models;
using StackOverflow.GPT.Core.Shared.Contracts;
using StackOverflow.GPT.Core.Shared.Persistence;

namespace StackOverflow.GPT.Core.Administration.Persistence;

public class AdministrationContext : BaseDbContext<AdministrationContext>
{
    public AdministrationContext(DbContextOptions<AdministrationContext> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options,
        domainEventsDispatcher, httpContextAccessor)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AccessPermission> AccessPermissions { get; set; }
}