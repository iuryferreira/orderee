namespace Orderee.Core.Shared;

public interface IDomainEvent
{
    public Guid? AggregateId { get; }
    public DateTime OccurredOn { get; }
}

abstract class AggregateRoot<T> where T : AggregateRoot<T>
{
    public Guid? Id { get; protected init; } = Guid.CreateVersion7();
    public IList<IDomainEvent> DomainEvents { get; } = [];

    public void ClearDomainEvents() => DomainEvents.Clear();

    protected void AddEvent(IDomainEvent domainEvent)
    {
        if (domainEvent is IAggregateEvent<T> aggregateEvent)
        {
            aggregateEvent.Apply((T)this);
        }
        else
        {
            throw new InvalidOperationException("Event not supported.");
        }

        DomainEvents.Add(domainEvent);
    }
    
    public static T Rebuild(IReadOnlyList<IDomainEvent> events)
    {
        var id = events.SingleOrDefault()?.AggregateId;
        if (id == null)
        {
            throw new ArgumentException("No events found.");
        }

        var aggregateRoot = Activator.CreateInstance<T>();
        foreach (var evt in events)
        {
            aggregateRoot.AddEvent(evt);
        }

        return aggregateRoot;
    }


}