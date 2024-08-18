using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IOrderCreatedEvent
{
    public Guid CorrelationId { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }
}