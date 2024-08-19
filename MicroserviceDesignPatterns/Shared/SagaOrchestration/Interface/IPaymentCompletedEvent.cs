using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IPaymentCompletedEvent
{
    public Guid CorrelationId { get;  }
}