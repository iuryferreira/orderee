using FluentValidation;

namespace Orderee.Core.Order.Domain.Entities;

class OrderItem
{
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}

class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}