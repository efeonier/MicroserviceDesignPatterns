using EventSourcing.Shared.Events.Interfaces;

namespace EventSourcing.Shared.Events.Concretes;

public class ProductDeletedEvent : IEvent
{
    public Guid Id { get; set; }
}