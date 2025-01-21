using Orderee.Core.Order.Domain.Entities;
using Orderee.Core.Shared;

namespace Orderee.Core.Order.Domain.Events;

class ItemAddedToOrderEvent(OrderItem item) : IDomainEvent, IAggregateEvent<Entities.Order>
{
    public Guid? AggregateId { get; private set; }
    public DateTime OccurredOn { get; protected set; } = DateTime.Now;
    
    public void Apply(Entities.Order aggregate)
    {
        AggregateId = aggregate.Id;
        aggregate.Items.Add(item);
    }
}