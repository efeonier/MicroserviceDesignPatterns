using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IPaymentFailedEvent
{
    public Guid CorrelationId { get; set; }
    public string Reason { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }
}