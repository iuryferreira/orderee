using Orderee.Core.Shared;

namespace Orderee.Core.Order.Domain.Events;

class OrderCreatedEvent : IDomainEvent, IAggregateEvent<Entities.Order>
{
    
    public void Apply(Entities.Order aggregate)
    {
        AggregateId = aggregate.Id;
    }

    public Guid? AggregateId { get; private set; }
    public DateTime OccurredOn { get; }  = DateTime.Now;
}