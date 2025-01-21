using Orderee.Core.Oder.Domain.Entities;
using Orderee.Core.Shared;

namespace Orderee.Core.Oder.Domain.Events;

class ItemAddedToOrderEvent(OrderItem item) : IDomainEvent, IAggregateEvent<Order>
{
    public Guid? AggregateId { get; private set; }
    public DateTime OccurredOn { get; protected set; } = DateTime.Now;
    
    public void Apply(Order aggregate)
    {
        AggregateId = aggregate.Id;
        aggregate.Items.Add(item);
    }
}