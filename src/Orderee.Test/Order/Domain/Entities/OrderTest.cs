using Orderee.Core.Oder.Domain.Entities;
using Orderee.Core.Oder.Domain.Events;

namespace Orderee.Test.Order.Domain.Entities;

public class OrderTest
{
    [Fact(DisplayName = "AddItem should add valid item when item does not exist")]
    public void AddItem_ShouldAddValidItem_WhenItemDoesNotExist()
    {
        // Arrange
        var order = Core.Oder.Domain.Entities.Order.Create();
        var validItem = new OrderItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            Price = 10
        };

        // Act
        order.AddItem(validItem);

        // Assert
        Assert.Equal(2, order.DomainEvents.Count);
        Assert.IsType<ItemAddedToOrderEvent>(order.DomainEvents.Last());
        Assert.Single(order.Items);
        Assert.Equal(validItem, order.Items.First());
    }

    [Fact(DisplayName = "AddItem should throw exception when item is invalid")]
    public void AddItem_ShouldThrowException_WhenItemIsInvalid()
    {
        // Arrange
        var order = Core.Oder.Domain.Entities.Order.Create();
        var invalidItem = new OrderItem
        {
            ProductId = Guid.Empty,
            Quantity = 0,
            Price = 0
        };

        // Act
        var act = () => order.AddItem(invalidItem);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact(DisplayName = "AddItem should throw exception when item already exists")]
    public void AddItem_ShouldThrowException_WhenItemAlreadyExists()
    {
        // Arrange
        var order = Core.Oder.Domain.Entities.Order.Create();
        var item = new OrderItem
        {
            ProductId = Guid.NewGuid(),
            Quantity = 1,
            Price = 10
        };

        order.AddItem(item);

        // Act
        var act = () => order.AddItem(item);

        // Assert

        Assert.Throws<InvalidOperationException>(act);
        Assert.Equal(2, order.DomainEvents.Count);
    }

    [Fact(DisplayName = "Create should add OrderCreatedEvent when called")]
    public void Create_ShouldAddOrderCreatedEvent_WhenCalled()
    {
        // Arrange & Act
        var order = Core.Oder.Domain.Entities.Order.Create();

        // Assert
        Assert.IsType<OrderCreatedEvent>(order.DomainEvents.First());
    }

    [Fact(DisplayName = "Create should generate new order when called")]
    public void Create_ShouldGenerateNewOrder_WhenCalled()
    {
        // Act
        var order = Core.Oder.Domain.Entities.Order.Create();

        // Assert

        Assert.NotNull(order.Id);
        Assert.Empty(order.Items);
    }
}