using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class PaymentFailedEvent: IPaymentFailedEvent
{
    public PaymentFailedEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public string Reason { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }

    public Guid CorrelationId { get; set; }
}