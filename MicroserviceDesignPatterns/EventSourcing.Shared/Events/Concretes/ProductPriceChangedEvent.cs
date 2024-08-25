using EventSourcing.Shared.Events.Interfaces;

namespace EventSourcing.Shared.Events.Concretes;

public class ProductPriceChangedEvent : IEvent
{
    public Guid Id { get; set; }
    public decimal ChangedPrice { get; set; }
}