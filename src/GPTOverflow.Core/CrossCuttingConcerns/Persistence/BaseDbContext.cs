using System.Security.Claims;
using GPTOverflow.Core.CrossCuttingConcerns.Contracts;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GPTOverflow.Core.CrossCuttingConcerns.Persistence;

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

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SoftDelete();
        Audit();
        var events = CollectEvents();
        var changesMade = base.SaveChanges(acceptAllChangesOnSuccess);
        if (changesMade > 0 && events.Any()) DomainEventsDispatcher.DispatchEvents(events);
        return changesMade;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SoftDelete();
        Audit();
        var events = CollectEvents();
        var changesMade = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        if (changesMade > 0 && events.Any()) await DomainEventsDispatcher.DispatchEventsAsync(events);
        return changesMade;
    }

    #region Change tracking

    private List<EntityEntry<AggregateRoot>> CollectEvents()
    {
        var domainEvents = this.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(HasDomainEvents
            ).ToList();

        return domainEvents;
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