using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IPaymentCompletedEvent : CorrelatedBy<Guid>
{
}