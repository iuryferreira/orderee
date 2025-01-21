namespace Orderee.Core.Shared;

interface IAggregateEvent<in T> where T : AggregateRoot<T>
{
    public Guid? AggregateId { get; }
    void Apply(T aggregate);
}