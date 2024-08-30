using EventSourcing.Shared.Events.Interfaces;

namespace EventSourcing.Shared.Events.Concretes;

public class ProductCreatedEvent : IEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int UserId { get; set; }
}