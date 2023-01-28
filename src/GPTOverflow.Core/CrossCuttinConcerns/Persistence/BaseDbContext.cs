using System.Security.Claims;
using GPTOverflow.Core.CrossCuttinConcerns.Contracts;
using GPTOverflow.Core.CrossCuttinConcerns.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GPTOverflow.Core.CrossCuttinConcerns.Persistence;

public abstract class BaseDbContext<T> : DbContext where T : DbContext
{
    protected readonly IDomainEventsDispatcher DomainEventsDispatcher;
    protected readonly IHttpContextAccessor HttpContextAccessor;

    protected BaseDbContext(DbContextOptions<T> options,
        IDomainEventsDispatcher domainEventsDispatcher, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        DomainEventsDispatcher = domainEventsDispatcher;
        HttpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddIgnoreSoftDeletedItemQueryFilter();
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SoftDelete();
        Audit();
        var changes = TrackChanges();
        var changesMade = await base.SaveChangesAsync(cancellationToken);
        if (changesMade > 0) await DomainEventsDispatcher.DispatchEventsAsync(changes);
        return changesMade;
    }

    public override int SaveChanges()
    {
        SoftDelete();
        Audit();
        var changes = TrackChanges();
        var changesMade = base.SaveChanges();
        if (changesMade > 0) DomainEventsDispatcher.DispatchEvents(changes);
        return changesMade;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SoftDelete();
        Audit();
        var changes = TrackChanges();
        var changesMade = base.SaveChanges(acceptAllChangesOnSuccess);
        if (changesMade > 0) DomainEventsDispatcher.DispatchEvents(changes);
        return changesMade;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SoftDelete();
        Audit();
        var changes = TrackChanges();
        var changesMade = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        if (changesMade > 0) await DomainEventsDispatcher.DispatchEventsAsync(changes);
        return changesMade;
    }

    #region Change tracking

    private List<EntityEntry<AggregateRoot>> TrackChanges()
    {
        var changes = this.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x =>
                HasDomainEvents(x) || HasBeenAddedOrRemoved(x)
            ).ToList();

        return changes;
    }

    private void Audit()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditable>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy =
                        HttpContextAccessor.HttpContext?.User?.GetValue(ClaimTypes.Name) ?? "Root";
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdatedBy =
                        HttpContextAccessor.HttpContext?.User?.GetValue(ClaimTypes.Name) ?? "Root";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdatedBy =
                        HttpContextAccessor.HttpContext?.User?.GetValue(ClaimTypes.Name) ?? "Root";
                    break;
            }
        }
    }

    private void SoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<ISoftDeletable>().ToList()
                     .Where(entry => entry.State == EntityState.Deleted))
        {
            entry.Entity.IsSoftDeleted = true;
            entry.State = EntityState.Modified;
            break;
        }
    }

    private static bool HasDomainEvents(EntityEntry<AggregateRoot> x)
    {
        return x.Entity.DomainEvents.Any();
    }

    private static bool HasBeenAddedOrRemoved(EntityEntry<AggregateRoot> x)
    {
        return x.State is EntityState.Added or EntityState.Deleted;
    }

    #endregion
}