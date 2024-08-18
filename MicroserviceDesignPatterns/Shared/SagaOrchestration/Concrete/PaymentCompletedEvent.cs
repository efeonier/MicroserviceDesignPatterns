using Shared.SagaOrchestration.Interface;

namespace Shared.SagaOrchestration.Concrete;

public class PaymentCompletedEvent: IPaymentCompletedEvent
{
    public PaymentCompletedEvent(Guid correlationId)
    {
        CorrelationId = correlationId;
    }
    public Guid CorrelationId { get; set; }
}