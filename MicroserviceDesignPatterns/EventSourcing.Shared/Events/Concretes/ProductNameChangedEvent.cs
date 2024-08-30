using EventSourcing.Shared.Events.Interfaces;

namespace EventSourcing.Shared.Events.Concretes;

public class ProductNameChangedEvent : IEvent
{
    public Guid Id { get; set; }
    public string ChangedName { get; set; } = string.Empty;
}