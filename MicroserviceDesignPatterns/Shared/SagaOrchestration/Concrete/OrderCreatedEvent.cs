using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class OrderCreatedEvent : IOrderCreatedEvent
{
    public OrderCreatedEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public List<OrderItemMessage> OrderItems { get; set; }
    public Guid CorrelationId { get; set; }
}