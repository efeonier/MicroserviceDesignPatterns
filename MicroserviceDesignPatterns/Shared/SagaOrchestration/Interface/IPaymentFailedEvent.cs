using MassTransit;

namespace Shared.SagaOrchestration.Interface;

public interface IPaymentFailedEvent: CorrelatedBy<Guid>
{
    public string Reason { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; }
}