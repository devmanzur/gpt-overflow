using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GPTOverflow.Core.CrossCuttingConcerns.Contracts
{
    /// <summary>
    /// All domain events generated as part of a *Write* operation
    /// are collected and dispatched by this
    /// </summary>
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(List<EntityEntry<AggregateRoot>> changes);
        void DispatchEvents(List<EntityEntry<AggregateRoot>> changes);
    }
}