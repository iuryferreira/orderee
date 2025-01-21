using Orderee.Core.Oder.Domain.Entities;
using Orderee.Core.Shared;

namespace Orderee.Core.Oder.Domain.Events;

class OrderCreatedEvent : IDomainEvent, IAggregateEvent<Order>
{
    
    public void Apply(Order aggregate)
    {
        AggregateId = aggregate.Id;
    }

    public Guid? AggregateId { get; private set; }
    public DateTime OccurredOn { get; }  = DateTime.Now;
}