using JetBrains.Annotations;
using Orderee.Core.Order.Domain.Events;
using Orderee.Core.Shared;

namespace Orderee.Core.Order.Domain.Entities;

class Order : AggregateRoot<Order>
{
    public IList<OrderItem> Items { get; private set; } = [];

    public Order(Guid? id) => Id = id;

    [UsedImplicitly]
    protected Order()
    {
    }

    public void AddItem(OrderItem item)
    {
        var validator = new OrderItemValidator();
        var validationResult = validator.Validate(item);

        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException("Invalid item.");
        }

        if (Items.Any(orderItem => orderItem.ProductId == item.ProductId))
        {
            throw new InvalidOperationException("Item already exists in order.");
        }

        AddEvent(new ItemAddedToOrderEvent(item));
    }


    public static Order Create()
    {
        var order = new Order();
        order.AddEvent(new OrderCreatedEvent());
        return order;
    }
}