using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class OrderCreatedEvent(Guid correlationId) : IOrderCreatedEvent
{
    public List<OrderItemMessage> OrderItems { get; set; } = [];

    public Guid CorrelationId { get; } = correlationId;
}