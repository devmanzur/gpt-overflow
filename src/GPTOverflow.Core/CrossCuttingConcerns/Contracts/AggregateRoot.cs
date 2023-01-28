using FluentValidation;
using GPTOverflow.Core.CrossCuttingConcerns.Utils;

namespace GPTOverflow.Core.CrossCuttingConcerns.Contracts;

/// <summary>
/// All entities that act as the entry point and manage other internal entities should extend this one
/// This facilitates system invariant validation for all internal entities as well as domain event generation
/// </summary>
public abstract class AggregateRoot : IEntity
{
    public Guid Id { get; set; }

    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();


    protected static void CheckRules<T, TV>(T instance) where T : class where TV : IValidator<T>, new() =>
        DomainValidator.Validate<T, TV>(instance);

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

}